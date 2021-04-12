// pch.cpp: source file corresponding to the pre-compiled header
#include "pch.h"
#include "Rupper.h"
#include "timeseries.h"
#include "HybridAnomalyDetector.h"
#include "DrawingWrapper.h"
#define _CRT_SECURE_NO_WARNINGS

extern "C" _declspec(dllexport)
void* getTimeSeries(const char* str)
{
    return (void*) new TimeSeries(str);
}


extern "C" _declspec(dllexport)
void callLearnNormal(HybridAnomalyDetector* hybrid, TimeSeries & ts)
{
    hybrid->learnNormal(ts);
}

extern "C" _declspec(dllexport)
void updateAnomaly(const TimeSeries & ts, HybridAnomalyDetector * hybrid, const char* description[])
{
	vector<AnomalyReport> vec = hybrid->detect(ts);
	for (int i = 0; i < vec.size(); i++) {
		description[i] = vec[i].description.c_str();
		//timeSteps[i] = vec[i].timeStep;
	}
}

extern "C" _declspec(dllexport)
void* getHybridDetector()
{
    return (void*) new HybridAnomalyDetector();
}


extern "C" _declspec(dllexport)
void getMyCorrelatedFeature(const TimeSeries &ts, char* src, char* buffer) {
    map<string, vector<float>> matrix = ts.getMapMatrix();
    string f1 = src, f2;
    int numOfFeatures = ts.getNumOfFeatures(), numOfLines = ts.getNumOfData(), start = 0;
    for (int i = 0; i < numOfFeatures; i++) {
        if (!strcmp(ts.getFeatureNames()[i].c_str(), f1.c_str())) {
            start = i + 1;
            break;
        }
    }
    float maxPearson = 0;
    for (; start < numOfFeatures; start++)
    {
        string tempFeature = ts.getFeatureNames()[start]; /// temp feature name - maybe will be feature2
        float tempPearson = pearson(matrix.at(f1).data(), matrix.at(tempFeature).data(), numOfLines);
        if (fabs(tempPearson) > fabs(maxPearson)) { /// if it's the biggest pearson (in absolute value) until now
            maxPearson = tempPearson; /// update if it is
            f2 = tempFeature; /// update also that it will be feature2
        }
    }
    if(fabs(maxPearson) > 0.9)
        strcpy(buffer, f2.c_str());
    else
        strcpy(buffer, "");
}


extern "C" _declspec(dllexport)
void* getLinearReg(const TimeSeries& ts, string f1, string f2) 
{
    Line line = linear_reg(ts.getMapMatrix()[f1].data(), ts.getMapMatrix()[f2].data(), ts.getNumOfData());
    return (void*) &line;
}

extern "C" _declspec(dllexport)
void* getRupperAnomaly(const TimeSeries& ts, HybridAnomalyDetector * hybrid) {
	vector<AnomalyReport> vec = hybrid->detect(ts);
	return new Rupper(vec);
}

extern "C" _declspec(dllexport)
int getSizeRupper(Rupper* rupper) {
	return rupper->getSize();
}

extern "C" _declspec(dllexport)
void getDescriptionRupper(Rupper* rupper, int i, char* buffer) {
	rupper->getDescByIndex(i, buffer);
}

extern "C" _declspec(dllexport)
int getTimeStepRupper(Rupper* rupper, int i) {
	return rupper->getTimeStepByIndex(i);
}


extern "C" _declspec(dllexport)
void* getDrawingRupper(TimeSeries & ts, char* f1, char* f2, HybridAnomalyDetector* hybrid) {
    vector<float> toReturn;
    int size = ts.getNumOfData();
    string s1 = f1, s2 = f2;
    vector<float> vecF1 = ts.getMapMatrix()[s1];
    vector<float> vecF2 = ts.getMapMatrix()[s2];
    float maxValue = vecF1[0];
    float minValue = vecF1[0];
    for (int i = 0; i < vecF1.size(); i++) {
        if (maxValue < vecF1[i])
            maxValue = vecF1[i];
        else if (minValue > vecF1[i])
            minValue = vecF1[i];
        if (maxValue < vecF2[i])
            maxValue = vecF2[i];
        else if (minValue > vecF2[i])
            minValue = vecF2[i];
    }
    toReturn.push_back(maxValue);
    toReturn.push_back(minValue);
    return new DrawingWrapper(toReturn, hybrid, s1, s2);
}

extern "C" _declspec(dllexport)
int getSizeDrawingWrapper(DrawingWrapper * drawingWrapper) {
    return drawingWrapper->getSize();
}

extern "C" _declspec(dllexport)
float getXValueByIndexDrawingWrapper(DrawingWrapper * drawingWrapper, int i) {
    return drawingWrapper->getXValueByIndex(i);
}

extern "C" _declspec(dllexport)
float getYValueByIndexDrawingWrapper(DrawingWrapper * drawingWrapper, int i) {
    return drawingWrapper->getYValueByIndex(i);
}


// When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
