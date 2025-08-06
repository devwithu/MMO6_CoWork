
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    [System.Serializable]

    public class JTestClass
    {
        public int i;
        public float f;
        public bool b;

        public Vector3 v;
        public string str;
        public int[] iArray;
        public List<int> iList = new List<int>();


        public JTestClass() { }


        public JTestClass(bool isSet)
        {

            if (isSet)

            {
                i = 10;
                f = 99.9f;
                b = true;

                v = new Vector3(39.56f, 21.2f, 6.4f);
                str = "JSON Test String";
                iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };


                for (int idx = 0; idx < 5; idx++)
                {
                    iList.Add(2 * idx);
                }
            }
        }


        public void Print()
        {
            Debug.Log("i = " + i);
            Debug.Log("f = " + f);
            Debug.Log("b = " + b);

            Debug.Log("v = " + v);
            Debug.Log("str = " + str);

            for (int idx = 0; idx < iArray.Length; idx++)
            {
                Debug.Log(string.Format("iArray[{0}] = {1}", idx, iArray[idx]));
            }

            for (int idx = 0; idx < iList.Count; idx++)
            {
                Debug.Log(string.Format("iList[{0}] = {1}", idx, iList[idx]));
            }
        }
    }

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }


    [System.Serializable]
    public class UJsonVector3Tester
    {
        public Vector3 v3;

        public UJsonVector3Tester() { }

        public UJsonVector3Tester(float f)
        {
            v3 = new Vector3(f, f, f);
        }

        public UJsonVector3Tester(Vector3 v)
        {
            v3 = v;
        }
    }

    [System.Serializable]
    public class TestMono : MonoBehaviour
    {
        public int i = 10;
        public Vector3 pos = new Vector3(1f, 2f, 3f);
    }


    // Start is called before the first frame update
    void Start()
    {
        JTestClass jtc = new JTestClass(true);
        string jsonData = ObjectToJson(jtc);
        Debug.Log(jsonData);

        var jtc2 = JsonToOject<JTestClass>(jsonData);
        jtc2.Print();

        //폴더생성 및 경로 통합
        string resourcesPath = Path.Combine(Application.persistentDataPath, "JTestClass");
        if (!Directory.Exists(resourcesPath))
        {
            Directory.CreateDirectory(resourcesPath);
        }

        CreateJsonFile(Application.persistentDataPath, "JTestClass", jsonData);

        var jtc3 = LoadJsonFile<JTestClass>(Application.persistentDataPath, "JTestClass");
        jtc3.Print();

        UJsonVector3Tester jt = new UJsonVector3Tester(transform.position);
        Debug.Log(JsonUtility.ToJson(jt));

        
        // 모노비헤이비어를 상속받는 클래스의 오브젝트 시리얼라이즈
        GameObject obj = new GameObject();
        obj.name = "TestMono 01";
        var t = obj.AddComponent<TestMono>();
        t.i = 333;
        t.pos = new Vector3(-939, -33, -22);
        var jd = JsonUtility.ToJson(obj.GetComponent<TestMono>());
        Debug.Log(jd);

        GameObject obj2 = new GameObject();
        obj2.name = "TestMono 02";
        var t2 = obj2.AddComponent<TestMono>();
        JsonUtility.FromJsonOverwrite(jd, t2);


    }

}
