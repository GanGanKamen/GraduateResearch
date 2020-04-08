// sample-dll3.cpp : DLL 用にエクスポートされる関数を定義します。
//

#include "pch.h"
#include "framework.h"
#include "sample-dll3.h"


// これは、エクスポートされた変数の例です
SAMPLEDLL3_API int nsampledll3=0;

// これは、エクスポートされた関数の例です。
SAMPLEDLL3_API int fnsampledll3(void)
{
    return 0;
}

int counter = 0;

SAMPLEDLL3_API int CountUp(void) {
	return counter++;
}

// これは、エクスポートされたクラスのコンストラクターです。
Csampledll3::Csampledll3()
{
    return;
}
