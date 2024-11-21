using Newtonsoft.Json;
namespace LayeredArchitecture.Common.ApiResponse
{
    public class DefineResponse
    {
        public string Status { get; set; }
        public ResponseItem ResponseItem { get; set; }

        #region init data
        public enum EnumCodes
        {
            /// <summary>
            /// Success
            /// </summary>
            R_CMN_200_01,
            /// <summary>
            /// Resource not found
            /// </summary>
            R_CMN_200_02,
            /// <summary>
            /// Invalid parameter
            /// </summary>
            R_CMN_400_01,
            /// <summary>
            /// Invalid tenant
            /// </summary>
            R_CMN_400_02,
            /// <summary>
            /// Validity period expired
            /// </summary>
            R_CMN_401_01,
            /// <summary>
            /// Username or password incorrect
            /// </summary>
            R_CMN_401_02,
            /// <summary>
            /// You do not have access. Please contact the system administrator
            /// </summary>
            R_CMN_403_01,
            /// <summary>
            /// This service unavailable for your tenant.Please contact the system administrator
            /// </summary>
            R_CMN_403_02,
            /// <summary>
            /// No data found
            /// </summary>
            R_CMN_404_01,
            /// <summary>
            /// You do not have permission to access this resource. Please contact the system administrator
            /// </summary>
            R_CMN_404_02,
            /// <summary>
            /// Data does not exist
            /// </summary>
            R_CMN_404_03,
            /// <summary>
            /// Tenant settings file not found.
            /// </summary>
            R_CMN_404_04,
            /// <summary>
            /// Data is exists
            /// </summary>
            R_CMN_409_01,
            /// <summary>
            /// {field}: malformed data {value}
            /// </summary>
            R_CMN_422_01,
            /// <summary>
            /// {field}: data cannot exceed {value}
            /// </summary>
            R_CMN_422_02,
            /// <summary>
            /// {field}: data cannot be less than {value}
            /// </summary>
            R_CMN_422_03,
            /// <summary>
            /// {field}: data cannot exceed {value} characters
            /// </summary>
            R_CMN_422_04,
            /// <summary>
            /// {field}: data cannot be less than {value} characters
            /// </summary>
            R_CMN_422_05,
            /// <summary>
            /// {field}: invalid transmitted data
            /// </summary>
            R_CMN_422_06,
            /// <summary>
            /// {field}: data cannot be empty
            /// </summary>
            R_CMN_422_07,
            /// <summary>
            /// email is exist
            /// </summary>
            R_CMN_422_08,
            /// <summary>
            /// The {field} must be less than the {field}
            /// </summary>
            R_CMN_422_09,
            /// <summary>
            /// Data in use cannot be deleted
            /// </summary>
            R_CMN_422_10,
            /// <summary>
            /// Data in use cannot be edited
            /// </summary>
            R_CMN_422_11,
            /// <summary>
            /// Server error. Please contact the system administrator
            /// </summary>
            R_CMN_500_01,
            /// <summary>
            /// The uploaded file is not in the correct format
            /// </summary>
            R_CMN_500_02,
            /// <summary>
            /// The uploaded file is too large. Maximum capacity {value}
            /// </summary>
            R_CMN_500_03,
            /// <summary>
            /// File download failed. Please contact the system administrator
            /// </summary>
            R_CMN_500_04,
            /// <summary>
            /// Uploading file failed. Please contact the system administrator
            /// </summary>
            R_CMN_500_05,
            /// <summary>
            /// File download failed. Please contact the system administrator
            /// </summary>
            R_CMN_500_06,
            /// <summary>
            /// Cannot log in to AWS. Please contact the system administrator
            /// </summary>
            R_CMN_500_07,
            /// <summary>
            /// Service unavailable
            /// </summary>
            R_CMN_503_01,

            /// <summary>
            /// Invalid table name
            /// </summary>
            R_541_422_01,
            /// <summary>
            /// The following IDs have errors.
            /// </summary>
            R_541_422_02,
            /// <summary>
            /// Export error
            /// </summary>
            R_541_500_01,

            /// <summary>
            /// BedManage not found
            /// </summary>
            R_BED_404_01,
            /// <summary>
            /// Empty bed not found
            /// </summary>
            R_BED_404_02,
            /// <summary>
            /// Start time is greater than End time
            /// </summary>
            R_BED_422_01,
            /// <summary>
            /// The bed is taken
            /// </summary>
            R_BED_422_02,
            /// <summary>
            /// This person is lying somewhere else
            /// </summary>
            R_BED_422_03,

            /// <summary>
            /// The template is incorrect.
            /// </summary>
            R_S25_400_01,
        }
        public static readonly List<ResponseItem> listResponseItems = new()
        {
            #region Common
            new ResponseItem{ Code = "R_CMN_200_01", Message = "Success"},
            new ResponseItem{ Code = "R_CMN_200_02", Message = "RESOURCE NOT FOUND!!!"},
            new ResponseItem{ Code = "R_CMN_400_01", Message = "Bad Request"},
            new ResponseItem{ Code = "R_CMN_401_01", Message = "Unauthorized"},
            new ResponseItem{ Code = "R_CMN_401_02", Message = "ユーザー名またはパスワードが間違っています。"},
            new ResponseItem{ Code = "R_CMN_403_01", Message = "Forbidden"},
            new ResponseItem{ Code = "R_CMN_404_01", Message = "Not Found"},
            new ResponseItem{ Code = "R_CMN_404_02", Message = "この資源にアクセスの権限がありません。管理者にお問い合わせください。"},
            new ResponseItem{ Code = "R_CMN_404_03", Message = "データが存在しません。"},
            new ResponseItem{ Code = "R_CMN_409_01", Message = "Data is exist"},
            new ResponseItem{ Code = "R_CMN_422_01", Message = "{field}: {value}形式ではありません。"},
            new ResponseItem{ Code = "R_CMN_422_02", Message = "{field}: データは{value}を超えることはできません。"},
            new ResponseItem{ Code = "R_CMN_422_03", Message = "{field}: データは{value}より小さくすることはできません。"},
            new ResponseItem{ Code = "R_CMN_422_04", Message = "{field}: データは{value}文字を超えることはできません。"},
            new ResponseItem{ Code = "R_CMN_422_05", Message = "{field}: データは{value}文字より少なくすることはできません。"},
            new ResponseItem{ Code = "R_CMN_422_06", Message = "{field}: 送信されたデータが不正です。"},
            new ResponseItem{ Code = "R_CMN_422_07", Message = "{field}: データを空白のままにすることはできません。"},
            new ResponseItem{ Code = "R_CMN_422_08", Message = "すでに登録済みのメールアドレスです。"},
            new ResponseItem{ Code = "R_CMN_422_09", Message = "{field} フィールドは、{value} フィールドより小さくなければなりません。"},
            new ResponseItem{ Code = "R_CMN_422_10", Message = "Data in use cannot be deleted."},
            new ResponseItem{ Code = "R_CMN_422_11", Message = "Data in use cannot be edited."},
            new ResponseItem{ Code = "R_CMN_500_01", Message = "A system error occurred."},
            new ResponseItem{ Code = "R_CMN_500_02", Message = "添付できるファイル形式は、.jpg、.jpeg、.png 、.heic 、.heif 、.pdf、.pptx、.doc、.docx、.xls、.xlsx、.psd、 .ai ファイル、となります。"},
            new ResponseItem{ Code = "R_CMN_500_03", Message = "ファイルサイズは {value} 以下にしてください。"},
            new ResponseItem{ Code = "R_CMN_500_04", Message = "ファイルのダウンロードが失敗しました。管理者にお問い合わせください。"},
            new ResponseItem{ Code = "R_CMN_500_05", Message = "ファイルのアップロードが失敗しました。管理者にお問い合わせください。"},
            new ResponseItem{ Code = "R_CMN_500_06", Message = "ファイルのダウンロードが失敗しました。管理者にお問い合わせください。"},
            new ResponseItem{ Code = "R_CMN_500_07", Message = "Cannot log into AWS "},
            new ResponseItem{ Code = "R_CMN_503_01", Message = "只今メンテナンス中です。"},
            #endregion

            #region Uncommon
            // Master table
            new ResponseItem{ Code = "R_541_422_01", Message = "Invalid table name"},
            new ResponseItem{ Code = "R_541_422_02", Message = "The following IDs have errors: {value}"},
            new ResponseItem{ Code = "R_541_500_01", Message = "Export error"},

            // Bed
            new ResponseItem{ Code = "R_BED_404_01", Message = "対象のデータは登録されてません"},
            new ResponseItem{ Code = "R_BED_404_02", Message = "対象の空きベッドはありません。検索方法を変えてください。"},
            new ResponseItem{ Code = "R_BED_422_01", Message = "利用終了日は、利用開始日より後の日時である必要があります。"},
            new ResponseItem{ Code = "R_BED_422_02", Message = "指定された期間でのベッドの利用はすでに予約されています。"},
            new ResponseItem{ Code = "R_BED_422_03", Message = "指定された利用者は、選択された期間内で既に別のベッドを利用しています。異なるベッドへの重複した予約はできません。"},

            //import
            new ResponseItem{ Code = "R_S25_400_01", Message = "テンプレートが正しくありません。"},
            #endregion
        };
        #endregion

        public DefineResponse(string status = null, ResponseItem? responseItem = null)
        {
            Status = string.IsNullOrEmpty(status) ? "500" : status;
            ResponseItem = responseItem ?? new ResponseItem();
        }

        /// <summary>
        /// Get error item from error code
        /// </summary>
        /// <param name="errorCode">Error code</param>
        public static ResponseItem? GetErrorListItem(EnumCodes errorCode)
        {
            var errorListItem = listResponseItems.FirstOrDefault(x => x.Code == errorCode.ToString());
            if (errorListItem != null)
            {
                var errorListItemsClone = JsonConvert.SerializeObject(errorListItem);
                var errorItem = JsonConvert.DeserializeObject<ResponseItem>(errorListItemsClone);
                return errorItem ?? null;
            }
            return null;
        }
    }
}
