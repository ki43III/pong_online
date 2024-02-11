using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class JsonData
{
    public string key;
    public string name;
    public int id;
    //public float P1x;
    public float P1y;
    //public float P1z;
    //public float P2x;
    public float P2y;
    //public float P2z;
    public float Bx;
    public float By;
    //public float Bz;
    public int P1P;
    public int P2P;
}

public class match_maker : MonoBehaviour
{
    // ローカルホストのポート番号
    public string localhostPort = "3000";
    private float _repeatSpan;      //繰り返す間隔
    public string serverUrl = "http://localhost:3000/";

    public GameObject Cube;
    public GameObject Cube2;
    public GameObject Ball;
    Vector3 Cube_position;
    Vector3 Cube2_position;
    Vector3 Ball_position;


    void Start()
    {
       
        _repeatSpan = 0.05f;                    //実行間隔を５に設定
        StartCoroutine(RepeatFunction());   //繰り返し処理を呼び出す
    }

    private void Update()
    {
        Cube_position = Cube.transform.position;
        Cube2_position = Cube2.transform.position;
        Ball_position = Ball.transform.position;
        
    }
    //繰り返し処理
    private IEnumerator RepeatFunction()
    {

        //無限ループ
        while (true)
        {
            yield return new WaitForSeconds(_repeatSpan); //n秒待つ
                                                          //ここで処理をする

            // 送信するデータをオブジェクトとして作成
            JsonData data = new JsonData
            {
                key = "1",
                name = "1",
                id = 123,
                P1y = Cube_position.y,
                Bx = Ball_position.x,
                By = Ball_position.y
                
            };

            Debug.Log(Cube_position);

            // データをJSON形式にシリアライズ
            string jsonData = JsonUtility.ToJson(data);

            // UnityWebRequestを作成してJSONデータを送信
            using (UnityWebRequest www = UnityWebRequest.Put(serverUrl, jsonData))
            {
                www.method = UnityWebRequest.kHttpVerbPOST;
                www.SetRequestHeader("Content-Type", "application/json");

                // レスポンスを受け取るためのDownloadHandlerを設定
                www.downloadHandler = new DownloadHandlerBuffer();

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // レスポンスの内容をログに出力
                    string responseText = www.downloadHandler.text;
                    JsonData responseData = JsonUtility.FromJson<JsonData>(responseText);

                    Cube2.transform.position = new Vector3(-6.5f, responseData.P2y, 0);
                    //Debug.Log("key" + responseData.key);

                    Debug.Log("サーバーレスポンス: " + responseText);
                }
            }
        }
    }

    

}
