// pch.cpp: source file corresponding to the pre-compiled header
#include "pch.h"
#include "Rupper.h"
#include "timeseries.h"
#include "HybridAnomalyDetector.h"
#define _CRT_SECURE_NO_WARNINGS

extern "C" _declspec(dllexport)
void* getTimeSeries(const char* str)
{
    //TimeSeries* ts = new TimeSeries(str);
    return (void*) new TimeSeries(str);
}


extern "C" _declspec(dllexport)
void callLearnNormal(HybridAnomalyDetector * hybrid, TimeSeries * ts)
{
    TimeSeries temp(*ts);
    hybrid->learnNormal(temp);
}

//extern "C" _declspec(dllexport)
//void getMyCorrelatedFeature(HybridAnomalyDetector* hybrid, char* str, char* buffer) {
//    for (int i = 0; i < hybrid->getNormalModel().size(); ++i)
//    {
//        if (!strcmp(hybrid->getNormalModel()[i].feature1.c_str(), str))
//        {
//            strcpy(buffer, hybrid->getNormalModel()[i].feature2.c_str());
//            break;
//        }
//    }
//}

extern "C" _declspec(dllexport)
void* getHybridDetector()
{
    //HybridAnomalyDetector* hybrid = new HybridAnomalyDetector();
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
    if (fabs(maxPearson) > 0.5)
        strcpy(buffer, f2.c_str());
    else
        strcpy(buffer, "");
}

extern "C" _declspec(dllexport)
void updateAnomaly(const TimeSeries & ts, HybridAnomalyDetector * hybrid, string description[], double timeSteps[])
{
	vector<AnomalyReport> vec = hybrid->detect(ts);
	for (int i = 0; i < vec.size(); i++) {
		description[i] = vec[i].description;
		timeSteps[i] = vec[i].timeStep;
	}
}

extern "C" _declspec(dllexport)
void* getLinearReg(const TimeSeries & ts, char* f1, char* f2)
{
    Line line = linear_reg(ts.getMapMatrix()[f1].data(), ts.getMapMatrix()[f2].data(), ts.getNumOfData());
    return (void*) &line;
}

extern "C" _declspec(dllexport)
float getYLine(Line l, float x) {
    return l.f(x);
}

extern "C" _declspec(dllexport)
void* getRupperAnomaly(const TimeSeries & ts, HybridAnomalyDetector * hybrid) {
    vector<AnomalyReport> vec = hybrid->detect(ts);
    return new Rupper(vec);
}

extern "C" _declspec(dllexport)
int getSizeRupper(Rupper * rupper) {
    return rupper->getSize();
}

extern "C" _declspec(dllexport)
void getDescriptionRupper(Rupper * rupper, int i, char* buffer) {
    rupper->getDescByIndex(i, buffer);
}

extern "C" _declspec(dllexport)
float getTimeStepRupper(Rupper * rupper, int i) {
    return rupper->getTimeStepByIndex(i);
}


//When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
