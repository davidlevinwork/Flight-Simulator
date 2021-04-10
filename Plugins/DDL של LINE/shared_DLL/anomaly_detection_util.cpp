#include "pch.h"
#include "anomaly_detection_util.h"

Line::Line() :a(0), b(0) {}
Line::Line(float a, float b) : a(a), b(b) {}
float Line::f(float x)
{
	return a * x + b;
}

Point::Point(float x, float y) :x(x), y(y) {}