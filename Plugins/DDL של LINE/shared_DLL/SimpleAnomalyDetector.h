//#include "anomaly_detection_util.h"
//#include "AnomalyDetector.h"
//#include "minCircle.h"
//#include <algorithm>
//#include <cmath>
//
//struct correlatedFeatures {
//    string feature1, feature2;  // names of the correlated features
//    float corrlation;
//    Line lin_reg;
//    float threshold;
//    Circle circle;
//};
//
//class SimpleAnomalyDetector : public TimeSeriesAnomalyDetector {
//protected:
//    vector<correlatedFeatures> cf;
//    float correlationThresholdLine = 0.9f;
//public:
//    SimpleAnomalyDetector() {}; /// constructor (empty)
//    virtual ~SimpleAnomalyDetector() {}; /// destructor (empty)
//    virtual void learnNormal(const TimeSeries& ts);
//    //virtual vector<AnomalyReport> detect(const TimeSeries& ts);
//    virtual vector<AnomalyReport> detectByLine(const TimeSeries& ts);
//    virtual vector<AnomalyReport> detectByCircle(const TimeSeries& ts);
//    //virtual pair<Line, float> getLineAndThreshold(vector<float>& x, vector<float>& y, int size, float corr);
//    //virtual bool isAnomaly(const TimeSeries& ts, const correlatedFeatures& cfStruct, int indexLine);
//    //virtual bool isCorrelatedEnough(float correlation);
//    virtual void makeStruct(const TimeSeries& ts, const string& feature1, const string& feature2, float maxPearson);
//    vector<correlatedFeatures> getNormalModel() { return cf; }
//    float getCorrelationThreshold() const { return correlationThresholdLine; }
//    void setCorrelationThreshold(float newThreshold) { correlationThresholdLine = newThreshold; }
//
//};
//
///// returns the max deviation between the two features in the struct.
//float getMaxDev(const vector<float>& x, const vector<float>& y, Line l, int numOfLines) {
//    float cfMaxDev = 0;
//    for (int i = 0; i < numOfLines; i++) {
//        float tempDev = dev(Point(x[i], y[i]), l);
//        if (tempDev > cfMaxDev) /// check if it is the biggest deviation until now
//            cfMaxDev = tempDev; /// update if it is
//    }
//    return cfMaxDev;
//}
//
///// the function checks if the correlation is higher enough for create a struct and returns true if it does.
////bool SimpleAnomalyDetector::isCorrelatedEnough(float correlation) {
////    if (fabs(correlation) > correlationThresholdLine)
////        return true;
////    return false;
////}
//
///// this is the offline learning level - learning from proper flight data the correlative features and their linear
///// regression line, their pearson and calculate their threshold.
////void SimpleAnomalyDetector::learnNormal(const TimeSeries &ts) {
////    map<string, vector<float>> matrix = ts.getMapMatrix();
////    string feature1, feature2, tempFeature;
////    int numOfFeatures = ts.getNumOfFeatures(), numOfLines = ts.getNumOfData();
////    float tempPearson;
////    for (int i = 0; i < numOfFeatures; i++) {
////        float maxPearson = 0;
////        feature1 = ts.getFeatureNames()[i];
////        for (int j = i + 1; j < numOfFeatures; j++) {
////            tempFeature = ts.getFeatureNames()[j]; /// temp feature name - maybe will be feature2
////            tempPearson = pearson(matrix.at(feature1).data(), matrix.at(tempFeature).data(), numOfLines);
////            if (fabs(tempPearson) > fabs(maxPearson)) { /// if it's the biggest pearson (in absolute value) until now
////                maxPearson = tempPearson; /// update if it is
////                feature2 = tempFeature; /// update also that it will be feature2
////            }
////        }
////        if (isCorrelatedEnough(maxPearson)) { /// make the struct only if the correlation between the two features is
////                                            /// higher enough
////            makeStruct(ts, feature1, feature2, maxPearson);
////        }
////    }
////}
//
//void SimpleAnomalyDetector::learnNormal(const TimeSeries& ts) {
//    map<string, vector<float>> matrix = ts.getMapMatrix();
//    string feature1, feature2;
//    int numOfFeatures = ts.getNumOfFeatures(), numOfLines = ts.getNumOfData();
//    for (int i = 0; i < numOfFeatures; i++) {
//        float maxPearson = 0;
//        feature1 = ts.getFeatureNames()[i];
//        for (int j = i + 1; j < numOfFeatures; j++) {
//            string tempFeature = ts.getFeatureNames()[j]; /// temp feature name - maybe will be feature2
//            float tempPearson = pearson(matrix.at(feature1).data(), matrix.at(tempFeature).data(), numOfLines);
//            if (fabs(tempPearson) > fabs(maxPearson)) { /// if it's the biggest pearson (in absolute value) until now
//                maxPearson = tempPearson; /// update if it is
//                feature2 = tempFeature; /// update also that it will be feature2
//            }
//        }
//        if (fabs(maxPearson) > 0.5) { /// make the struct only if the correlation between the two features is
//                                            /// higher enough
//            makeStruct(ts, feature1, feature2, maxPearson);
//        }
//    }
//}
//
//void SimpleAnomalyDetector::makeStruct(const TimeSeries& ts, const string& f1, const string& f2, float maxPearson) {
//    map<string, vector<float>> matrix = ts.getMapMatrix();
//    Line line = linear_reg(matrix.at(f1).data(), matrix.at(f2).data(), ts.getNumOfData());
//    float threshold = getMaxDev(matrix.at(f1), matrix.at(f2), line, ts.getNumOfData()) * 1.1f;
//    Circle circle = findMinCircle(matrix.at(f1), matrix.at(f2), ts.getNumOfData());
//    correlatedFeatures cfs{ /// make the struct with all the data we calculate
//            f1, f2, maxPearson, line, threshold, circle
//    };
//    cf.push_back(cfs);
//}
//
/////// the function creates the struct and pushes it to the cf vector.
////void SimpleAnomalyDetector::makeStruct(const TimeSeries& ts, const string& f1, const string& f2, float maxPearson) {
////    map<string, vector<float>> matrix = ts.getMapMatrix();
////    /// getLineAndThreshold returns the line and the threshold. lAndT.first is the line. lAndT.second is the threshold.
////    pair<Line, float> lAndT = getLineAndThreshold(matrix.at(f1), matrix.at(f2), ts.getNumOfData(), maxPearson);
////    correlatedFeatures cfs{ /// make the struct with all the data we calculate
////            f1, f2, maxPearson, lAndT.first, lAndT.second
////    };
////    cf.push_back(cfs);
////}
/////// the function creates and returns the line and the threshold for the struct.
////pair<Line, float> SimpleAnomalyDetector::getLineAndThreshold(vector<float>& x, vector<float>& y, int size, float corr) {
////    Line line = linear_reg(x.data(), y.data(), size);
////    float threshold = getMaxDev(x, y, line, size) * 1.1f;
////    return make_pair(line, threshold);
////}
//
/////// the function returns true if there is an anomaly in this line (if the deviation is too high). false otherwise.
////bool SimpleAnomalyDetector::isAnomaly(const TimeSeries& ts, const correlatedFeatures& cfStruct, int indexLine) {
////    map<string, vector<float>> matrix = ts.getMapMatrix();
////    Point p(matrix.at(cfStruct.feature1)[indexLine], matrix.at(cfStruct.feature2)[indexLine]);
////    float tempDev = dev(p, cfStruct.lin_reg); /// check the deviation in the line
////    if (tempDev > cfStruct.threshold)
////        return true;
////    return false;
////}
////
/////// this is the online anomaly detection level - reading the live flight data line by line and if we found a deviation
/////// we will report about it with the details of the deviation.
////vector<AnomalyReport> SimpleAnomalyDetector::detect(const TimeSeries& ts) {
////    vector<AnomalyReport> arVec; /// the AnomalyReport vector we will return
////    map<string, vector<float>> matrix = ts.getMapMatrix();
////    for (auto& cfStruct : cf) {
////        for (int i = 0; i < ts.getNumOfData(); i++) {
////            if (isAnomaly(ts, cfStruct, i)) /// if there is an anomaly we will report and push it to the arVec
////                arVec.emplace_back(cfStruct.feature1 + "-" + cfStruct.feature2, long(i + 1));
////        }
////    }
////    return arVec;
////}
//
//
//
//////vector<AnomalyReport> SimpleAnomalyDetector::detectByLine(const TimeSeries& ts) {
//////    vector<AnomalyReport> arVec; /// the AnomalyReport vector we will return
//////    map<string, vector<float>> matrix = ts.getMapMatrix();
//////    for (auto& cfStruct : cf) {
//////        for (int i = 0; i < ts.getNumOfData(); i++) {
//////            Point p(matrix.at(cfStruct.feature1)[i], matrix.at(cfStruct.feature2)[i]);
//////            float tempDev = dev(p, cfStruct.lin_reg); /// check the deviation in the line
//////            if (tempDev > cfStruct.threshold)
//////                arVec.emplace_back(cfStruct.feature1 + "-" + cfStruct.feature2, long(i + 1));
//////        }
//////    }
//////    return arVec;
//////}
//////
//////vector<AnomalyReport> SimpleAnomalyDetector::detectByCircle(const TimeSeries& ts) {
//////    vector<AnomalyReport> arVec; /// the AnomalyReport vector we will return
//////    map<string, vector<float>> matrix = ts.getMapMatrix();
//////    for (auto& cfStruct : cf) {
//////        for (int i = 0; i < ts.getNumOfData(); i++) {
//////            Point p(matrix.at(cfStruct.feature1)[i], matrix.at(cfStruct.feature2)[i]);
//////            if (isPointNotInCircle(cfStruct.circle, p))
//////                arVec.emplace_back(cfStruct.feature1 + "-" + cfStruct.feature2, long(i + 1));
//////        }
//////    }
//////    return arVec;
//////}