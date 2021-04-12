#pragma once
#include "pch.h"
#include "AnomalyDetector.h"

class DrawingWrapper {
	vector<float> _vecXValues;
	vector<float> _vecYValues;
	Line _line;
public:
	DrawingWrapper(vector<float> vecAnomaly, HybridAnomalyDetector * hybrid, string f1, string f2) {
		bool flag = false;
		int size = vecAnomaly.size();
		for (int i = 0; i < size; i++) {
			_vecXValues.push_back(vecAnomaly[i]);
		}
		size = hybrid->getNormalModel().size();
		for (int i = 0; i < size; i++) {
			if (!hybrid->getNormalModel()[i].feature1.compare(f1) && !hybrid->getNormalModel()[i].feature2.compare(f2)) {
				_line = hybrid->getNormalModel()[i].lin_reg;
				flag = true;
				break;
			}
		}
		if (flag) {
			size = vecAnomaly.size();
			for (int i = 0; i < size; i++) {
				_vecYValues.emplace_back(_line.f(_vecXValues[i]));
			}
		}
	}

	int getSize();

	float getXValueByIndex(int i);
	float getYValueByIndex(int i);

};


int DrawingWrapper::getSize() {
	return _vecYValues.size();
}


float DrawingWrapper::getXValueByIndex(int i) {
	return _vecXValues[i];
}

float DrawingWrapper::getYValueByIndex(int i) {
	return _vecYValues[i];
}