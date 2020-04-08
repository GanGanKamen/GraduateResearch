// 以下の ifdef ブロックは、DLL からのエクスポートを容易にするマクロを作成するための
// 一般的な方法です。この DLL 内のすべてのファイルは、コマンド ラインで定義された SAMPLEDLL3_EXPORTS
// シンボルを使用してコンパイルされます。このシンボルは、この DLL を使用するプロジェクトでは定義できません。
// ソースファイルがこのファイルを含んでいる他のプロジェクトは、
// SAMPLEDLL3_API 関数を DLL からインポートされたと見なすのに対し、この DLL は、このマクロで定義された
// シンボルをエクスポートされたと見なします。
#ifdef SAMPLEDLL3_EXPORTS
#define SAMPLEDLL3_API __declspec(dllexport)
#else
#define SAMPLEDLL3_API __declspec(dllimport)
#endif

// このクラスは dll からエクスポートされました
class SAMPLEDLL3_API Csampledll3 {
public:
	Csampledll3(void);
	// TODO: メソッドをここに追加します。
};

extern SAMPLEDLL3_API int nsampledll3;

SAMPLEDLL3_API int fnsampledll3(void);

extern "C" {
	SAMPLEDLL3_API int CountUp(void);
}