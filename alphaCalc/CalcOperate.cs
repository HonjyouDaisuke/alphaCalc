namespace alphaCalc;

internal enum Operates
{
    None = -1, 
    Ca,
    C,
    DoubleZero,
    Dot,
    Pm,
    Mult,
    Plus,
    Back,
    Per,
    Minus,
    Equal,
}

internal class CalcOperate
{
    internal Operates OperateCode { get; set; } = Operates.None;

    /// <summary>
    /// 演算記号を文字で返す
    /// </summary>
    /// <returns></returns>
    internal string GetOperateString()
        => GetOperateStringFromCode(OperateCode);

    /// <summary>
    /// 演算記号コードから文字を取得
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    internal static string GetOperateStringFromCode(Operates c)
        => c switch
        {
            Operates.Plus => "＋",
            Operates.Minus => "－",
            Operates.Per => "÷",
            Operates.Mult => "×",
            Operates.Equal => "＝",
            _ => "",
        };

    /// <summary>
    /// 演算をセットする
    /// </summary>
    /// <param name="op">演算記号</param>
    /// <returns></returns>
    internal void SetOperate(string op)
        => OperateCode = op switch
        {
            "plus" => Operates.Plus,
            "minus" => Operates.Minus,
            "per" => Operates.Per,
            "mult" => Operates.Mult,
            "equal" => Operates.Equal,
            "ca" => Operates.Ca,
            "c" => Operates.C,
            "plusminus" => Operates.Pm,
            _ => Operates.None,
        };

    /// <summary>
    /// クラス複製用のコピー作成
    /// </summary>
    /// <returns>CalcOperateクラス</returns>
    internal CalcOperate ShallowCopy()
        => (CalcOperate)MemberwiseClone();
}
