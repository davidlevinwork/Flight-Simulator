#pragma once
#include "AnomalyDetector.h"
#include "pch.h"

class Rupper {
	vector<AnomalyReport> _vecAnomaly;
public:
	Rupper(vector<AnomalyReport> vecAnomaly) {
		for (int i = 0; i < vecAnomaly.size(); i++) {
			_vecAnomaly.push_back(vecAnomaly[i]);
		}
	}

	int getSize();

	void getDescByIndex(int i, char* buffer);

	float getTimeStepByIndex(int i);
};

int Rupper::getSize() {
	return _vecAnomaly.size();
}

void Rupper::getDescByIndex(int i, char* buffer) {
	memcpy(buffer, _vecAnomaly[i].description.c_str(), _vecAnomaly[i].description.length());
	buffer[_vecAnomaly[i].description.length()] = '\0';
}

float Rupper::getTimeStepByIndex(int i) {
	return _vecAnomaly[i].timeStep;
}