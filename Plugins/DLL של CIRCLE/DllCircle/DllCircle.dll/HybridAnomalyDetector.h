#pragma once
#include "AnomalyDetector.h"
#include "timeseries.h"
#include "minCircle.h"
#include <algorithm>
#include <cmath>
#include "pch.h"

struct correlatedFeatures {
    string feature1, feature2;  // names of the correlated features
    float corrlation;
    Line lin_reg;
    float threshold;
    Circle circle;
};

class HybridAnomalyDetector {
    vector<correlatedFeatures> cf;
public:


    virtual void learnNormal(const TimeSeries& ts);
    //virtual vector<anomalyreport> detect(const timeseries& ts);
    virtual vector<AnomalyReport> detect(const TimeSeries& ts);
    //virtual pair<line, float> getlineandthreshold(vector<float>& x, vector<float>& y, int size, float corr);
    //virtual bool isanomaly(const timeseries& ts, const correlatedfeatures& cfstruct, int indexline);
    //virtual bool iscorrelatedenough(float correlation);
    virtual void makeStruct(const TimeSeries& ts, const string& feature1, const string& feature2, float maxpearson);
    vector<correlatedFeatures> getNormalModel() { return cf; }
    HybridAnomalyDetector() {};
    virtual ~HybridAnomalyDetector() {};

};

void HybridAnomalyDetector::learnNormal(const TimeSeries& ts) {
    map<string, vector<float>> matrix = ts.getMapMatrix();
    string feature1, feature2;
    int numOfFeatures = ts.getNumOfFeatures(), numOfLines = ts.getNumOfData();
    for (int i = 0; i < numOfFeatures; i++) {
        float maxPearson = 0;
        feature1 = ts.getFeatureNames()[i];
        for (int j = i + 1; j < numOfFeatures; j++) {
            string tempFeature = ts.getFeatureNames()[j]; /// temp feature name - maybe will be feature2
            float tempPearson = pearson(matrix.at(feature1).data(), matrix.at(tempFeature).data(), numOfLines);
            if (fabs(tempPearson) > fabs(maxPearson)) { /// if it's the biggest pearson (in absolute value) until now
                maxPearson = tempPearson; /// update if it is
                feature2 = tempFeature; /// update also that it will be feature2
            }
        }
        if (fabs(maxPearson) > 0.5) { /// make the struct only if the correlation between the two features is
                                            /// higher enough
            makeStruct(ts, feature1, feature2, maxPearson);
        }
    }
}

void HybridAnomalyDetector::makeStruct(const TimeSeries& ts, const string& f1, const string& f2, float maxPearson) {
    Line line(0, 0);
    float threshold = 0;
    Circle circle = findMinCircle(ts.getMapMatrix().at(f1), ts.getMapMatrix().at(f2), ts.getNumOfData());
    circle.radius *= 1.1f;
    correlatedFeatures cfs{ /// make the struct with all the data we calculate
            f1, f2, maxPearson, line, threshold, circle
    };
    cf.push_back(cfs);
}

vector<AnomalyReport> HybridAnomalyDetector::detect(const TimeSeries& ts) {
    vector<AnomalyReport> arVec; /// the AnomalyReport vector we will return
    map<string, vector<float>> matrix = ts.getMapMatrix();
    for (auto& cfStruct : cf) {
        for (int i = 0; i < ts.getNumOfData(); i++) {
            Point p(matrix.at(cfStruct.feature1)[i], matrix.at(cfStruct.feature2)[i]);
            if (isPointNotInCircle(cfStruct.circle, p))
                arVec.emplace_back(cfStruct.feature1 + "-" + cfStruct.feature2, long(i + 1));
        }
    }
    return arVec;
}




