using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CLDC_DataCore.DataBase
{
    /// <summary>
    /// 在误差限获取模块化中专用
    /// </summary>
    public struct IDAndValue
    {
        /// <summary>
        /// 数据库中对应ID
        /// </summary>
        public long id;
        /// <summary>
        /// 数据库中对应值
        /// </summary>
        public string Value;
        public override string ToString()
        {
            return Value;
        }
    }

    public class clsWcLimitDataControl
    {

        /// <summary>
        /// ACCESS数据库连接字符串常数
        /// </summary>
        const string CONST_ACCESS = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Jet OLEDB:DataBase Password=;Data Source=";

        private OleDbConnection _Conn;

        public clsWcLimitDataControl()
        {
            OpenDb();
        }
        //~clsWcLimitDataControl()
        //{
        //    try
        //    {
        //        Close();
        //    }
        //    catch
        //    { }
        //}


        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void OpenDb()
        {
            if(!System.IO.File.Exists(CLDC_DataCore.Function.File.GetPhyPath(CLDC_DataCore.Const.Variable.CONST_WCLIMIT)))
            {
                CLDC_DataCore.DataBase.CreateMdb NewMdb = new CLDC_DataCore.DataBase.CreateMdb(CLDC_DataCore.Function.File.GetPhyPath(CLDC_DataCore.Const.Variable.CONST_WCLIMIT));

                if (!NewMdb.CreateWcLimitMDB())
                {
                    System.Windows.Forms.MessageBox.Show("创建误差限数据库失败，请联系服务人员检查...", "打开数据库出错", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }

            if (_Conn != null && _Conn.State != System.Data.ConnectionState.Closed)
            {
                _Conn.Close();
                OleDbConnection.ReleaseObjectPool();
            }

            _Conn = new OleDbConnection();
            _Conn.ConnectionString = CONST_ACCESS + CLDC_DataCore.Function.File.GetPhyPath(CLDC_DataCore.Const.Variable.CONST_WCLIMIT);
            try
            {
                _Conn.Open();
            }
            catch (Exception e)
            {

                CLDC_DataCore.Function.ErrorLog.Write(e);
                //throw e;
            }
        }

        public void Close()
        {
            try
            {
                if (_Conn != null && _Conn.State != System.Data.ConnectionState.Closed)
                {
                    _Conn.Close(); 
                    _Conn.Dispose();
                }
                _Conn = null;
                OleDbConnection.ReleaseObjectPool();
            }
            catch(InvalidOperationException ioe)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ioe);
            }
        }


        #region -------------------------------获取一个项目的值返回用IDAndValue结构体--------------------------------
        /// <summary>
        /// 获取误差限名称
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IDAndValue getWcLimitNameValue(string Name)
        {
            return getValue(Name, "WcLimitName");
        }

        /// <summary>
        /// 获取规程名称
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IDAndValue getGuiChengValue(string Name)
        {
            return getValue(Name, "GuiCheng");
        }
        /// <summary>
        /// 获取等级名称
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IDAndValue getDjValue(string Name)
        {
            return getValue(Name, "MeterLevel");
        }

        /// <summary>
        /// 获取ID和值
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private IDAndValue getValue(string Name, string TableName)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            IDAndValue _TmpValue = new IDAndValue();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = "Select ID,[Name] From " + TableName + " Where [Name]='" + Name + "'";
            try
            {
                OleDbDataReader _Reader = _Cmd.ExecuteReader();

                if (_Reader.Read())
                {
                    _TmpValue.id = long.Parse(_Reader[0].ToString());
                    _TmpValue.Value = _Reader[1].ToString();
                }
                else
                {
                    _TmpValue.id = -1;
                    _TmpValue.Value = Name;
                }

                _Reader.Close();
                _Reader.Dispose();
                _Reader = null;
                _Cmd.Dispose();

                return _TmpValue;
            }
            catch
            {
                _Cmd.Dispose();

                return _TmpValue;
            }
            finally
            {
                _Cmd.Dispose();
                _Cmd = null;
            }

        }

        #endregion

        #region -------------------------获取一个表字段的集合--------------------------------
        /// <summary>
        /// 获取规程名称集合
        /// </summary>
        /// <returns></returns>
        public List<IDAndValue> GuiChengNames()
        {
            return getListString("GuiCheng");
        }
        /// <summary>
        /// 获取等级名称集合
        /// </summary>
        /// <returns></returns>
        public List<IDAndValue> DjNames()
        {
            return getListString("MeterLevel");
        }
        /// <summary>
        /// 获取内控误差限名称
        /// </summary>
        /// <returns></returns>
        public List<IDAndValue> WcLimitName()
        {
            return getListString("WcLimitName");
        }


        /// <summary>
        /// 根据表名称返回数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private List<IDAndValue> getListString(string TableName)
        {
            if (_Conn == null) return new List<IDAndValue>();
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = "Select ID,Name From " + TableName;
            if (_Conn.State != System.Data.ConnectionState.Open)
            {
                _Conn.Open();
            }
            OleDbDataReader _Reader = _Cmd.ExecuteReader();

            List<IDAndValue> _Lst = new List<IDAndValue>();

            while (_Reader.Read())
            {
                IDAndValue _TmpValue = new IDAndValue();
                _TmpValue.id = long.Parse(_Reader[0].ToString());
                _TmpValue.Value = _Reader[1].ToString();
                _Lst.Add(_TmpValue);
            }

            _Reader.Close();
            _Reader.Dispose();
            _Reader = null;
            _Cmd.Dispose();
            _Cmd = null;

            return _Lst;
        }


        #endregion

        #region ------------------------------插入一个字段------------------------------------
        /// <summary>
        /// 插入一个误差限名称
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns>在数据表中对应ID</returns>
        public int InsertWcLimitName(string Name)
        {
            return InsertValue(Name, "WcLimitName");
        }

        /// <summary>
        /// 插入一个规程名称
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns>在数据表中对应ID</returns>
        public int InsertGuiChengName(string Name)
        {
            return InsertValue(Name, "GuiCheng");
        }

        /// <summary>
        /// 插入一个等级名称
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns>在数据表中对应ID</returns>
        public int InsertDjName(string Name)
        {
            return InsertValue(Name, "MeterLevel");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="TableName">数据表名称</param>
        /// <returns>在数据表中对应ID</returns>
        private int InsertValue(string Name, string TableName)
        {
            OleDbCommand _Cmd = new OleDbCommand();

            _Cmd.Connection = _Conn;

            _Cmd.CommandText = string.Format("Insert INTO {0}([Name]) Values('{1}')", TableName, Name);

            try
            {
                _Cmd.ExecuteNonQuery();
            }
            catch
            {
                _Cmd.Dispose();
                return -1;
            }

            _Cmd.CommandText = string.Format("select Max(ID) From {0}", TableName);

            try
            {
                int RowIndex = int.Parse(_Cmd.ExecuteScalar().ToString());
                _Cmd.Dispose();
                return RowIndex;
            }
            catch
            {
                _Cmd.Dispose();
                return -1;
            }
        }

        #endregion


        #region ---------------------------删除一个字段------------------------------
        /// <summary>
        /// 移除误差限名称
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool RemoveWcLimitName(string Name)
        {
            return this.RemoveValue(Name, "WcLimitName");
        }

        /// <summary>
        /// 移除一个规程
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool RemoveGuiCheng(string Name)
        {
            return this.RemoveValue(Name, "GuiCheng");
        }
        /// <summary>
        /// 移除一个等级
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool RemoveDj(string Name)
        {
            return this.RemoveValue(Name, "MeterLevel");
        }

        /// <summary>
        /// 移除名称
        /// </summary>
        /// <param name="Name">表中字段值</param>
        /// <param name="TableName">数据表名称</param>
        /// <returns></returns>
        private bool RemoveValue(string Name, string TableName)
        {
            OleDbCommand _Cmd = new OleDbCommand();

            _Cmd.Connection = _Conn;
            _Cmd.CommandText = string.Format("Delete From {0} Where [Name]='{1}'", TableName, Name);

            try
            {
                _Cmd.ExecuteNonQuery();
                _Cmd.Dispose();
                return true;
            }
            catch
            {
                _Cmd.Dispose();
                return false;
            }
            finally { _Cmd.Dispose(); }
        }

        #endregion

        /// <summary>
        /// 修改误差限
        /// </summary>
        /// <param name="WcLimitID">误差限名称ID</param>
        /// <param name="GuiChengID">规程ID</param>
        /// <param name="DjID">等级ID</param>
        /// <param name="YjID">元件ID</param>
        /// <param name="Hgq">是否经互感器接入</param>
        /// <param name="YouGong">是否有功</param>
        /// <param name="xIbID">电流倍数ID</param>
        /// <param name="GlysID">功率因素ID</param>
        /// <param name="WcLimit">误差限值</param>
        public void SaveWcx(long WcLimitID
                            , long GuiChengID
                            , long DjID
                            , int YjID
                            , bool Hgq
                            , bool YouGong
                            , int xIbID
                            , int GlysID
                            , string WcLimit)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = string.Format(@"Update WcLimit Set Limit='{0}' Where WcLimitNameID={1:D} And 
                                            GuiChengID={2:D} And MeterLevelID={3:D} And YjID={4:D} And GlysID={5:D} And 
                                            CurrentID={6:D} And Hgq={7} And YouGong={8}"
                                          , WcLimit              //误差限值
                                          , WcLimitID            //误差限名称ID
                                          , GuiChengID           //规程ID
                                          , DjID                 //等级ID
                                          , YjID                 //元件ID
                                          , GlysID               //功率因素ID
                                          , xIbID                //电流倍数ID
                                          , Hgq.ToString()              //是否经互感器接入
                                          , YouGong.ToString());        //是否有功

            try
            {
                _Cmd.ExecuteNonQuery();
            }
            catch { }
            finally { _Cmd.Dispose(); _Cmd = null; }
        }

        /// <summary>
        /// 修改偏差限
        /// </summary>
        /// <param name="WcLimitID">误差限名称ID</param>
        /// <param name="GuiChengID">规程ID</param>
        /// <param name="DjID">等级ID</param>
        /// <param name="PcLimit">偏差限值</param>
        public void SavePcx(long WcLimitID
                            , long GuiChengID
                            , long DjID
                            , float PcLimit)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = string.Format(@"Update PcLimit Set PcLimit='{0:F}' Where WcLimitNameID={1:D} And 
                                            GuiChengID={2:D} And MeterLevelID={3:D}"
                                          , PcLimit              //偏差限值
                                          , WcLimitID            //误差限名称ID
                                          , GuiChengID           //规程ID
                                          , DjID);               //等级ID


            try
            {
                _Cmd.ExecuteNonQuery();
            }
            catch { }
            finally { _Cmd.Dispose(); _Cmd = null; }

        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="WcLimitName">误差限名称</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">有无功</param>
        /// <param name="Arr_Row">行字符串(电流倍数....)</param>
        /// <param name="Arr_Column">列字符串(功率因素.....)</param>
        /// <returns></returns>
        public System.Data.DataTable getDataSource(IDAndValue WcLimitName
                                                   , IDAndValue GuiChengName
                                                   , IDAndValue Dj
                                                   , CLDC_Comm.Enum.Cus_PowerYuanJian Yj
                                                   , bool Hgq
                                                   , bool YouGong
                                                   , string[] Arr_Row
                                                   , string[] Arr_Column)
        {
            System.Data.DataTable _NewDataTable = new System.Data.DataTable();
            if (GuiChengName.Value.ToUpper().Substring(0, 6) == "JJG596") YouGong = true;           //如果是电子式规程，强制设置为有功

            if (Arr_Column.Length < 1)              // 功率因素
                return new System.Data.DataTable();

            for (int i = 0; i < Arr_Column.Length; i++)
                _NewDataTable.Columns.Add("Col_" + i);

            if (Arr_Row.Length < 1)                   //电流倍数
                return new System.Data.DataTable();            

            for (int i = 0; i < Arr_Row.Length; i++)      //循环电流倍数
            {
                string[] _Values = new string[Arr_Column.Length];     //行误差限数组

                for (int j = 0; j < Arr_Column.Length; j++)
                {
                    OleDbCommand _Cmd = new OleDbCommand();
                    _Cmd.Connection = _Conn;

                    int _IbID = int.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(Arr_Row[i]));

                    int _GlysID = int.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Arr_Column[j]));

                    _Cmd.CommandText = "Select Limit From WcLimit Where WcLimitNameID=" + WcLimitName.id
                                                              + " And GuiChengID=" + GuiChengName.id
                                                              + " And MeterLevelID=" + Dj.id
                                                              + " And YjID=" + (int)Yj
                                                              + " And CurrentID=" + _IbID
                                                              + " And GlysID=" + _GlysID
                                                              + " And Hgq=" + Hgq.ToString() + " And YouGong=" + YouGong.ToString();

                    try
                    {
                        OleDbDataReader _ReadValue = _Cmd.ExecuteReader();
                        if (!_ReadValue.Read())
                        {
                            _ReadValue.Close();
                            _Values[j] = Wcx(Arr_Row[i], GuiChengName.Value, Dj.Value, Yj, Arr_Column[j], Hgq, YouGong);
                            if (_Values[j].IndexOf('.') == -1)
                                _Values[j] = float.Parse(_Values[j]).ToString("F1");
                            _Values[j] = string.Format("+{0}|-{1}", _Values[j], _Values[j]);
                            _Cmd.CommandText = string.Format(@"Insert Into WcLimit(WcLimitNameID,GuiChengID,MeterLevelID,YjID,GlysID,CurrentID,Hgq,YouGong,Limit) 
                                             Values 
                                             ({0},{1},{2},{3},{4},{5},{6},{7},'{8}')"
                                             , WcLimitName.id
                                             , GuiChengName.id
                                             , Dj.id
                                             , (int)Yj
                                             , _GlysID
                                             , _IbID
                                             , Hgq.ToString()
                                             , YouGong.ToString()
                                             , _Values[j]);
                            try
                            {
                                _Cmd.ExecuteNonQuery();
                            }
                            catch { }

                        }
                        else
                            _Values[j] = _ReadValue[0].ToString();
                        _ReadValue.Dispose();
                        _Cmd.Dispose();
                    }
                    catch
                    {
                        _Values[j] = Wcx(Arr_Row[i], GuiChengName.Value, Dj.Value, Yj, Arr_Column[j], Hgq, YouGong);
                        if (_Values[j].IndexOf('.') == -1)
                            _Values[j] = float.Parse(_Values[j]).ToString("F1");
                        _Values[j] = string.Format("+{0}|-{1}", _Values[j], _Values[j]);
                    }
                    finally
                    {
                        _Cmd.Dispose();
                        _Cmd = null;
                    }                    

                }

                _NewDataTable.Rows.Add(_Values);
            }

            return _NewDataTable;
        }
        /// <summary>
        /// 获取一个检定点的误差限
        /// </summary>
        /// <param name="WcLimitName">误差限名称</param>
        /// <param name="GuiChengName">规程</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">是否有功</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <returns></returns>
        public string GetWcx(IDAndValue WcLimitName
                           , IDAndValue GuiChengName
                           , IDAndValue Dj
                           , CLDC_Comm.Enum.Cus_PowerYuanJian Yj
                           , bool Hgq
                           , bool YouGong
                           , IDAndValue Glys
                           , IDAndValue xIb)
        {
            if (GuiChengName.Value.ToUpper().Substring(0, 6) == "JJG596") YouGong = true;           //如果是电子式规程，强制设置为有功

            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;

            _Cmd.CommandText = "Select Limit From WcLimit Where WcLimitNameID=" + WcLimitName.id
                                                      + " And GuiChengID=" + GuiChengName.id
                                                      + " And MeterLevelID=" + Dj.id
                                                      + " And YjID=" + (int)Yj
                                                      + " And CurrentID=" + xIb.id
                                                      + " And GlysID=" + Glys.id
                                                      + " And Hgq=" + Hgq.ToString() + " And YouGong=" + YouGong.ToString();
            try
            {
                OleDbDataReader _Read = _Cmd.ExecuteReader();
                if (!_Read.Read())
                {
                    string _TmpWc = Wcx(xIb.Value, GuiChengName.Value, Dj.Value, Yj, Glys.Value, Hgq, YouGong);
                    if (_TmpWc.IndexOf('.') == -1)
                        _TmpWc = float.Parse(_TmpWc).ToString("F1");
                    _Read.Close();
                    _Read.Dispose();
                    _Read = null;
                    _Cmd.Dispose();
                    return string.Format("+{0}|-{1}", _TmpWc, _TmpWc);
                }
                else
                {
                    string strResult = _Read[0].ToString();
                    _Read.Close();
                    _Read.Dispose();
                    _Read = null;
                    _Cmd.Dispose();
                    return strResult;
                }
            }
            catch
            {
                string _TmpWc = Wcx(xIb.Value, GuiChengName.Value, Dj.Value, Yj, Glys.Value, Hgq, YouGong);
                if (_TmpWc.IndexOf('.') == -1)
                    _TmpWc = float.Parse(_TmpWc).ToString("F1");
                _Cmd.Dispose();
                return string.Format("+{0}|-{1}", _TmpWc, _TmpWc);

            }
            finally
            {
                _Cmd.Dispose();
                _Cmd = null;
            }

        }

        /// <summary>
        /// 获取偏差限值
        /// </summary>
        /// <param name="WcLimitName">误差限名称</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <returns>返回偏差限值</returns>
        public string getPcxValue(IDAndValue WcLimitName, IDAndValue GuiChengName, IDAndValue Dj)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = string.Format(@"Select PcLimit From PcLimit Where WcLimitNameID={0} And GuiChengID={1} And MeterLevelID={2}"
                                             , WcLimitName.id, GuiChengName.id, Dj.id);
            try
            {
                OleDbDataReader _ReadValue = _Cmd.ExecuteReader();
                string _TmpValue = "";
                if (!_ReadValue.Read())
                {
                    _TmpValue = Pcx(Dj.Value).ToString();

                }
                else
                {
                    _TmpValue = _ReadValue[0].ToString();
                }
                _ReadValue.Close();
                _ReadValue.Dispose();
                _ReadValue = null;
                _Cmd.Dispose();
                return string.Format("+{0}|0", _TmpValue);
            }
            catch { return Pcx(Dj.Value).ToString(); }
            finally
            {
                _Cmd.Dispose();

                _Cmd = null;
            }

        }


        /// <summary>
        /// 获取偏差限
        /// </summary>
        /// <param name="WcLimitName">误差限名称</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <returns>返回偏差限值</returns>
        public string getPcx(IDAndValue WcLimitName, IDAndValue GuiChengName, IDAndValue Dj)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;
            _Cmd.CommandText = string.Format(@"Select PcLimit From PcLimit Where WcLimitNameID={0} And GuiChengID={1} And MeterLevelID={2}"
                                             , WcLimitName.id, GuiChengName.id, Dj.id);
            try
            {
                OleDbDataReader _ReadValue = _Cmd.ExecuteReader();
                string _TmpValue = "";
                if (!_ReadValue.Read() || _ReadValue[0].ToString() == "")
                {
                    _TmpValue = Pcx(Dj.Value).ToString();

                    _Cmd.CommandText = string.Format(@"Insert into PcLimit(WcLimitNameID,GuiChengID,MeterLevelID,[PcLimit])Values({0},{1},{2},'{3}')"
                                                    , WcLimitName.id, GuiChengName.id, Dj.id, _TmpValue);
                    try
                    {
                        _Cmd.ExecuteNonQuery();
                    }
                    catch { }
                }
                else
                {
                    _TmpValue = _ReadValue[0].ToString();
                }
                _ReadValue.Close();
                _ReadValue.Dispose();
                _ReadValue = null;
                _Cmd.Dispose();
                return _TmpValue;
            }
            catch { return Pcx(Dj.Value).ToString(); }
            finally { _Cmd.Dispose(); _Cmd = null; }
        }


        /// <summary>
        /// 获取偏差限
        /// </summary>
        /// <param name="Dj">等级字符串不带S（0.2）</param>
        /// <returns></returns>
        public static float Pcx(string Dj)
        {
            if (!CLDC_DataCore.Function.Number.IsNumeric(Dj))
                return 1F * 0.2F;
            return float.Parse(Dj) * 0.2F;
        }

        /// <summary>
        /// 新增误差限 zzg soinlove@126.com
        /// </summary>
        /// <param name="WcLimitID">误差限名称ID</param>
        /// <param name="GuiChengID">规程ID</param>
        /// <param name="DjID">等级ID</param>
        /// <param name="YjID">元件ID</param>
        /// <param name="Hgq">是否经互感器接入</param>
        /// <param name="YouGong">是否有功</param>
        /// <param name="xIbID">电流倍数ID</param>
        /// <param name="GlysID">功率因素ID</param>
        /// <param name="WcLimit">误差限值</param>
        public void SetWcx(IDAndValue WcLimitName
                           , IDAndValue GuiChengName
                           , IDAndValue Dj
                           , CLDC_Comm.Enum.Cus_PowerYuanJian Yj
                           , bool Hgq
                           , bool YouGong
                           , IDAndValue Glys
                           , IDAndValue xIb
                            , string WcLimit)
        {
            OleDbCommand _Cmd = new OleDbCommand();
            _Cmd.Connection = _Conn;

            _Cmd.CommandText = "Select Limit From WcLimit Where WcLimitNameID=" + WcLimitName.id
                                                              + " And GuiChengID=" + GuiChengName.id
                                                              + " And MeterLevelID=" + Dj.id
                                                              + " And YjID=" + (int)Yj
                                                              + " And CurrentID=" + xIb.id
                                                              + " And GlysID=" + Glys.id
                                                              + " And Hgq=" + Hgq.ToString() + " And YouGong=" + YouGong.ToString();
            OleDbDataReader _ReadValue = _Cmd.ExecuteReader();
            if (_ReadValue.Read())
            {
                _ReadValue.Close();
                _Cmd.CommandText = string.Format(@"Update WcLimit Set Limit='{0}' Where WcLimitNameID={1:D} And 
                                            GuiChengID={2:D} And MeterLevelID={3:D} And YjID={4:D} And GlysID={5:D} And 
                                            CurrentID={6:D} And Hgq={7} And YouGong={8}"
                                              , WcLimit              //误差限值
                                              , WcLimitName.id            //误差限名称ID
                                              , GuiChengName.id           //规程ID
                                              , Dj.id                 //等级ID
                                              , (int)Yj                 //元件ID
                                              , Glys.id               //功率因素ID
                                              , xIb.id                //电流倍数ID
                                              , Hgq.ToString()              //是否经互感器接入
                                              , YouGong.ToString());        //是否有功
            }
            else
            {
                _Cmd.CommandText = string.Format(@"Insert Into WcLimit(WcLimitNameID,GuiChengID,MeterLevelID,YjID,GlysID,CurrentID,Hgq,YouGong,Limit) 
                                             Values 
                                             ({0},{1},{2},{3},{4},{5},{6},{7},'{8}')"
                                             , WcLimitName.id
                                             , GuiChengName.id
                                             , Dj.id
                                             , (int)Yj
                                             , Glys.id
                                             , xIb.id
                                             , Hgq.ToString()
                                             , YouGong.ToString()
                                             , WcLimit);
            }
            _ReadValue.Close();
            //try
            //{
            _Cmd.ExecuteNonQuery();
            //GlobalUnit.g_MsgControl.OutMessage(DateTime.Now.ToString(), true);
            //}
            //catch { }
            //finally { _Cmd.Dispose(); }
        }

        /// <summary>
        /// 获取误差限
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">是否有功</param>
        /// <returns></returns>
        public static string Wcx(string xIb, string GuiChengName, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq, bool YouGong)
        {
            switch (GuiChengName.ToUpper())
            {
                case "JJG596-1999":
                    {
                        return getLimit_JJG596_1999(xIb, Dj, Yj, glys, Hgq);
                    }
                case "JJG307-1988":
                    {
                        return getGy(xIb, "JJG307-1988", Dj, Yj, glys, Hgq, YouGong);
                    }
                case "JJG307-2006":
                    {
                        return getGy(xIb, "JJG307-2006", Dj, Yj, glys, Hgq, YouGong);
                    }
                case "JJG596-2012"://TODO:596-2012 默认有功电能表，2、3级的无功电能表没做
                    {
                        return getLimit_JJG596_2012(xIb, Dj, Yj, glys, Hgq);
                    }
                default:
                    return Dj;
            }
        }

        private static string getLimit_JJG596_1999(string xIb, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            switch (Dj)
            {
                #region
                case "0.02":
                    return getdz002(xIb, Yj, glys, Hgq);
                case "0.05":
                    return getdz005(xIb, Yj, glys, Hgq);
                case "0.1":
                    return getdz01(xIb, Yj, glys, Hgq);
                case "0.2":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz02(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "0.3";
                        else
                            return "0.4";
                    }
                case "0.5":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz05(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "0.6";
                        else
                            return "1.0";
                    }
                case "1.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz10(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "2.0";
                        else
                            return "2.0";
                    }
                case "2.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz20(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "3.0";
                        else
                            return "3.0";
                    }
                case "3.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz30(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "4.0";
                        else
                            return "4.0";
                    }
                default:
                    return Dj;

                #endregion
            }
        }

        private static string getLimit_JJG596_2012(string xIb, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            switch (Dj)
            {
                #region
                case "0.02":
                    return getdz002(xIb, Yj, glys, Hgq);
                case "0.05":
                    return getdz005(xIb, Yj, glys, Hgq);
                case "0.1":
                    return getdz01(xIb, Yj, glys, Hgq);
                case "0.2":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz02(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "0.3";
                        else
                            return "0.4";
                    }
                case "0.5":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz05(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "0.6";
                        else
                            return "1.0";
                    }
                case "1.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz10(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "2.0";
                        else
                            return "2.0";
                    }
                case "2.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz20(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "3.0";
                        else
                            return "3.0";
                    }
                case "3.0":
                    if ((int)Yj == 1)  ///合元
                    {
                        return getdz30(xIb, Yj, glys, Hgq);
                    }
                    else
                    {
                        if (glys == "1.0")
                            return "4.0";
                        else
                            return "4.0";
                    }
                default:
                    return Dj;

                #endregion
            }
        }

        #region 
        /// <summary>
        /// 电子式0.02级
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <returns></returns>
        private static string getdz002(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if ((int)Yj == 1)
            {
                if (glys == "1.0")
                {
                    _WcLimit = "0.02";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.04";
                }
                else
                {
                    _WcLimit = "0.02";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.03";
                    else if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.03";
                    if (glys == "0.25L")
                        _WcLimit = "0.04";
                    else if (glys == "0.5C")
                        _WcLimit = "0.03";
                }
            }
            else
            {
                _WcLimit = "0.03";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.04";
            }
            return _WcLimit;

        }
        /// <summary>
        /// 电子式0.05级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz005(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if ((int)Yj == 1)
            {
                if (glys == "1.0")
                {
                    _WcLimit = "0.05";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.1";
                }
                else
                {
                    _WcLimit = "0.05";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.15";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.075";
                    if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.075";
                    if (glys.ToUpper() == "0.5C")
                        _WcLimit = "0.1";
                    if (glys.ToUpper() == "0.25L")
                        _WcLimit = "0.15";
                }
            }
            else
            {
                _WcLimit = "0.075";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.1";
            }
            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.1级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz01(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if ((int)Yj == 1)
                if (glys == "1.0")
                {
                    _WcLimit = "0.1";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.2";
                }
                else
                {
                    _WcLimit = "0.1";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.3";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.15";
                    if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.15";
                    if (glys == "0.5C")
                        _WcLimit = "0.2";
                    if (glys == "0.25L")
                        _WcLimit = "0.3";
                }
            else
            {
                _WcLimit = "0.15";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.2";
            }
            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.2级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz02(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.2";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.2";
                }
            else if (glys.ToUpper() == "0.5C" || glys.ToUpper() == "0.25L")
                _WcLimit = "0.5";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.5";
                    else
                        _WcLimit = "0.3";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.5";
                    else
                        _WcLimit = "0.3";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.5级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz05(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.5";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.5";
                }
            else if (glys.ToUpper() == "0.5C" || glys.ToUpper() == "0.25L")
                _WcLimit = "1";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.6";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.6";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式1级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz10(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
            else if (glys.ToUpper() == "0.5C")
                _WcLimit = "2.5";
            else if (glys.ToUpper() == "0.25L")
                _WcLimit = "3.5";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式2级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz20(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式3级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz30(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
            }

            return _WcLimit;
        }

        /// <summary>
        /// 获取感应式电能表的误差限
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">有无功</param>
        /// <returns></returns>
        private static string getGy(string xIb, string GuiChengName, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq, bool YouGong)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "0.1ib")
            { }
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (YouGong)        //有功
            {
                if ((int)Yj == 1)      //合元
                {
                    if (glys == "1.0")
                    {
                        _WcLimit = Dj;
                        if (GuiChengName.ToUpper() == "JJG307-2006" && Hgq)
                        {
                            if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                                _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                        }
                        else if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                            _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                    }
                    else if (glys.ToUpper() == "0.5C")
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "1.5";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "2.5";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                        }
                    }
                    else if (glys.ToUpper() == "0.25L")
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "2.5";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "4.5";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "4.5";
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "0.8";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "1.0";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "2.0";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "2.0";
                                    break;
                                }
                        }
                        if (GuiChengName.ToUpper() == "JJG307-2006" && Hgq)
                        {
                            if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                                _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                        }
                        else if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                            _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                    }
                }
                else
                {
                    switch (Dj)
                    {
                        case "0.5":
                            {
                                _WcLimit = "1.5";
                                break;
                            }
                        case "1.0":
                            {
                                _WcLimit = "2.0";
                                break;
                            }
                        case "2.0":
                            {
                                _WcLimit = "2.0";
                                break;
                            }
                        default:
                            {
                                _WcLimit = "3.0";
                                break;
                            }

                    }
                    if (GuiChengName.ToUpper() == "JJG307-1988")
                        if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 1F)
                            _WcLimit = (float.Parse(_WcLimit) + 1F).ToString();
                }
            }
            else//无功
            {
                if ((int)Yj == 1)           //合元
                {
                    if (glys == "1.0")
                    {
                        _WcLimit = Dj;
                        if (GuiChengName.ToUpper() == "JJG307-2006" && Hgq)
                        {
                            if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                                _WcLimit = (float.Parse(_WcLimit) + 1.0F).ToString();
                        }
                        else if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                            _WcLimit = (float.Parse(_WcLimit) + 1.0F).ToString();
                    }
                    else if (glys.ToUpper() == "0.25C" || glys.ToUpper() == "0.25L")
                    {
                        _WcLimit = (float.Parse(Dj) * 2F).ToString();
                    }
                    else
                    {
                        if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                            _WcLimit = (float.Parse(Dj) + 2.0F).ToString();
                        else
                            _WcLimit = Dj;

                        if (GuiChengName.ToUpper() == "JJG307-2006")
                        {
                            if (Hgq)
                            {
                                if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                                    _WcLimit = (float.Parse(_WcLimit) - 1F).ToString();
                                else if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                                    _WcLimit = (float.Parse(_WcLimit) - 2F).ToString();
                            }
                            else if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                                _WcLimit = (float.Parse(_WcLimit) - 1F).ToString();
                        }
                    }
                }
                else
                    _WcLimit = (float.Parse(Dj) + 1.0F).ToString();
            }

            return _WcLimit;

        }
        #endregion

    }
}
