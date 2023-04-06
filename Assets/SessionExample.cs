using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReBase;

public class SessionExample : MonoBehaviour
{
    private Session session;
    private int insertedCount = 0;
    private string professionalId = "MrTrotta2010";
    private string patientId = "007";
    private string firstSessionId = "";

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        Movement movement = new Movement(
            description: "Eu sou o primeiro movimento da Sess�o",
            sessionId: "test1",
            label: "NewAPITest",
            fps: Application.targetFrameRate,
            professionalId: professionalId,
            patientId: patientId,
            articulations: new int[] { 1, 2 },
            articulationData: null
        );

        movement.AddRegister(new Register(
            new Dictionary<int, Vector3>()
            {
                {  1, new Vector3(1f, 1f, 1f) },
                {  2, new Vector3(2f, 2f, 2f) }
            }
        ));

        StartCoroutine(RESTClient.Instance.InsertMovement(OnInserted, movement));
    }

    public void OnInserted(APIResponse response)
    {
        Debug.Log($"Inserted: {response}");
        insertedCount++;

        if (insertedCount == 2)
        {
            firstSessionId = response.movement.sessionId;
            StartCoroutine(RESTClient.Instance.FetchSessions(OnFetch, professionalId: professionalId, patientId: patientId));
            return;
        }

        Movement movement = new Movement(
            description: "Eu sou o segundo movimento da Sess�o",
            sessionId: "test1",
            label: "NewAPITest",
            fps: Application.targetFrameRate,
            professionalId: professionalId,
            patientId: patientId,
            articulations: new int[] { 1, 2 }
        );

        movement.AddRegister(new Register(
            new Dictionary<int, Vector3>()
            {
                {  1, new Vector3(1f, 1f, 1f) },
                {  2, new Vector3(2f, 2f, 2f) }
            }
        ));

        StartCoroutine(RESTClient.Instance.InsertMovement(OnInserted, movement));
    }

    public void OnFetch(APIResponse response)
    {
        Debug.Log($"Downloaded: {response}");

        foreach (SerializableSession serializableSession in response.sessions)
		{
            if (serializableSession.id == firstSessionId)
			{
                session = new Session(serializableSession);
                break;
            }
		}

        session.title = "Atualizando a Sess�o";
        StartCoroutine(RESTClient.Instance.UpdateSession(OnUpdated, session));
    }

    public void OnUpdated(APIResponse response)
    {
        Debug.Log($"Updated: {response}");
        string id = response.session.id ?? session.id;
        StartCoroutine(RESTClient.Instance.DeleteSession(OnDeleted, id));
    }

    public void OnDeleted(APIResponse response)
    {
        Debug.Log($"Deleted: {response}");

        session = new Session(
            id: "test2",
            title: "Teste de Sess�o 2",
            description: "Todos os movimentos da Sess�o ser�o inseridos de uma vez",
            professionalId: professionalId,
            patientId: patientId,
            movements: new Movement[2]
            {
                new Movement(
                    label: "firstMovement",
                    fps: Application.targetFrameRate,
                    articulations: new int[] { 1, 2 },
                    articulationData: new Register[1] {
                        new Register(
                            new Dictionary<int, Vector3>()
                            {
                                {  1, new Vector3(1f, 1f, 1f) },
                                {  2, new Vector3(2f, 2f, 2f) }
                            }
                        )
                    }
                ),
                new Movement(
                    label: "secondMovement",
                    fps: Application.targetFrameRate,
                    articulations: new int[] { 1, 2 },
                    articulationData: new Register[1] {
                        new Register(
                            new Dictionary<int, Vector3>()
                            {
                                {  1, new Vector3(1f, 1f, 1f) },
                                {  2, new Vector3(2f, 2f, 2f) }
                            }
                        )
                    }
                )
            }
        );

        StartCoroutine(RESTClient.Instance.InsertSession(OnBulkInsert, session));
    }

    public void OnBulkInsert(APIResponse response)
	{
        Debug.Log($"Inserted: {response}");
        StartCoroutine(RESTClient.Instance.FindSession(OnFind, session.id));
    }

    public void OnFind(APIResponse response)
    {
        Debug.Log($"Found: {response}");
        StartCoroutine(RESTClient.Instance.DeleteSession(End, response.session.id));
    }

    public void End(APIResponse response)
    {
        Debug.Log($"Deleted: {response}");
    }
}
