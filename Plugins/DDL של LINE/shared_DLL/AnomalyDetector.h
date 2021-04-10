#pragma once
#include "timeseries.h"
using namespace std;

class AnomalyReport {
public:
    const string description;
    const long timeStep;
    AnomalyReport(string description, long timeStep) :
        description(description), timeStep(timeStep) {}
};

class TimeSeriesAnomalyDetector {
public:
    virtual void learnNormal(const TimeSeries& ts) = 0;
    virtual vector<AnomalyReport> detect(const TimeSeries& ts) = 0;
    virtual ~TimeSeriesAnomalyDetector() {}
};
