#pragma once
#include "pch.h"
#include "AnomalyDetector.h"
#include "minCircle.h"

class DrawingWrapper {
	vector<float> _vecXValues;
	vector<float> _vecYValues;
	Circle _circle;
public:
	DrawingWrapper(Circle c) {
		_circle = c;
		float x = _circle.center.x;
		float y = _circle.center.y;
		float radius = _circle.radius;
		float minX = x - radius;
		float maxX = x + radius;
		float minY = y - radius;
		float maxY = y + radius;
		//float pie = 3.14159265358979323846 / 50.0;
		for (int i = 0; i < 100; i++) {
			_vecXValues.emplace_back(x + (radius * cos((3.6f * (float)i) * (3.14159265358979323846 / 180.0))));
			_vecYValues.emplace_back(y + (radius * sin((3.6f * (float)i) * (3.14159265358979323846 / 180.0))));
		}
		cout << "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! radius     " << radius << endl;
		cout << "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! x     " << x << endl;
		cout << "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! y     " << y << endl;
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