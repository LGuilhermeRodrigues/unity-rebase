﻿using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

namespace ReBase
{
	[Serializable]
	public class Session
	{
		private string _id;
		private string _title;
		private string _description;
		private string _professionalId;
		private int _patientSessionNumber;

		private int _appCode;
		private string _appData;

		private string _patientId;
		private int _patientAge;
		private float _patientHeight;
		private float _patientWeight;
		private string _mainComplaint;
		private string _historyOfCurrentDesease;
		private string _historyOfPastDesease;
		private string _diagnosis;
		private string _relatedDeseases;
		private string _medications;
		private string _physicalEvaluation;

		private List<Movement> _movements;


		public string id { get => _id; }
		public string title { get => _title; set => _title = value; }
		public string description { get => _description; set => _description = value; }
		public string professionalId { get => _professionalId; set => _professionalId = value; }
		public int patientSessionNumber { get => _patientSessionNumber; set => _patientSessionNumber = value; }
		public int appCode { get => _appCode; set => _appCode = value; }
		public string appData { get => _appData; set => _appData = value; }
		public string patientId { get => _patientId; set => _patientId = value; }
		public int patientAge { get => _patientAge; set => _patientAge = value; }
		public float patientHeight { get => _patientHeight; set => _patientHeight = value; }
		public float patientWeight { get => _patientWeight; set => _patientWeight = value; }
		public string mainComplaint { get => _mainComplaint; set => _mainComplaint = value; }
		public string historyOfCurrentDesease { get => _historyOfCurrentDesease; set => _historyOfCurrentDesease = value; }
		public string historyOfPastDesease { get => _historyOfPastDesease; set => _historyOfPastDesease = value; }
		public string diagnosis { get => _diagnosis; set => _diagnosis = value; }
		public string relatedDeseases { get => _relatedDeseases; set => _relatedDeseases = value; }
		public string medications { get => _medications; set => _medications = value; }
		public string physicalEvaluation { get => _physicalEvaluation; set => _physicalEvaluation = value; }
		public List<Movement> movements
		{
			get => _movements;
			set => _movements = value;
		}

		public Session(string title = "", string description = "", string professionalId = "", List<Movement> movements = null, int patientSessionNumber = 0,
						int appCode = 0, string appData = "", string patientId = "", int patientAge = 0, float patientHeight = 0f, float patientWeight = 0f,
						string mainComplaint = "", string historyOfCurrentDesease = "", string historyOfPastDesease = "", string diagnosis = "",
						string relatedDeseases = "", string medications = "", string physicalEvaluation = "")
		{
			_title = title;
			_description = description;
			_professionalId = professionalId;
			_patientSessionNumber = patientSessionNumber;
			_appCode = appCode;
			_appData = appData;
			_patientId = patientId;
			_patientAge = patientAge;
			_patientHeight = patientHeight;
			_patientWeight = patientWeight;
			_mainComplaint = mainComplaint;
			_historyOfCurrentDesease = historyOfCurrentDesease;
			_historyOfPastDesease = historyOfPastDesease;
			_diagnosis = diagnosis;
			_relatedDeseases = relatedDeseases;
			_medications = medications;
			_physicalEvaluation = physicalEvaluation;

			if (movements != null) _movements = movements;
		}

		public Session(string title = "", string description = "", string professionalId = "", Movement[] movements = null, int patientSessionNumber = 0,
						int appCode = 0, string appData = "", string patientId = "", int patientAge = 0, float patientHeight = 0f, float patientWeight = 0f,
						string mainComplaint = "", string historyOfCurrentDesease = "", string historyOfPastDesease = "", string diagnosis = "",
						string relatedDeseases = "", string medications = "", string physicalEvaluation = "")
		{
			_title = title;
			_description = description;
			_professionalId = professionalId;
			_patientSessionNumber = patientSessionNumber;
			_appCode = appCode;
			_appData = appData;
			_patientId = patientId;
			_patientAge = patientAge;
			_patientHeight = patientHeight;
			_patientWeight = patientWeight;
			_mainComplaint = mainComplaint;
			_historyOfCurrentDesease = historyOfCurrentDesease;
			_historyOfPastDesease = historyOfPastDesease;
			_diagnosis = diagnosis;
			_relatedDeseases = relatedDeseases;
			_medications = medications;
			_physicalEvaluation = physicalEvaluation;

			if (movements != null) _movements = new List<Movement>(movements);
		}

		//public Session(SerializableSession movement)
		//{
		//	ConvertSerializableMovement(movement);
		//}

		//public Session(LegacySerializableSession movement)
		//{
		//	ConvertSerializableMovement(movement);
		//}

		//public Session(string sessionJson, bool legacySession = false)
		//{
		//	if (legacySession)
		//	{
		//		SerializableSession auxSession = JsonUtility.FromJson<SerializableSession>(movementJson);
		//		ConvertSerializableSession(auxSession, legacySessio);
		//	}
		//	else
		//	{
		//		LegacySerializableSession auxSession = JsonUtility.FromJson<SerializableSession>(movementJson);
		//		ConvertSerializableSession(auxSession, legacySessio);
		//	}
		//}

		public string ToJson()
		{
			CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

			string strMovements = "[";

			foreach (Movement movement in _movements)
			{
				strMovements += $"{movement.ToJson()},";
			}
			strMovements = $"{strMovements.TrimEnd(',')}]";

			return $"{{\"session\":{{\"id\":\"{_id}\"," +
				$"\"title\":\"{_title}\"," +
				$"\"description\":\"{_description}\"," +
				$"\"professionalId\":\"{_professionalId}\"," +
				$"\"patientSessionNumber\":{_patientSessionNumber}," +
				"\"app\":{" +
				$"\"code\":{_appCode}," +
				$"\"data\":\"{_appData}\"}}," +
				"\"patient\":{" +
				$"\"id\":\"{_patientId}\"," +
				$"\"age\":{_patientAge}," +
				$"\"height\":{_patientHeight}," +
				$"\"weight\":{_patientWeight}}}," +
				"\"medicalData\":{" +
				$"\"mainComplaint\":\"{_mainComplaint}\"," +
				$"\"historyOfCurrentDesease\":\"{_historyOfCurrentDesease}\"," +
				$"\"historyOfPastDesease\":\"{_historyOfPastDesease}\"," +
				$"\"diagnosis\":\"{_diagnosis}\"," +
				$"\"relatedDeseases\":\"{_relatedDeseases}\"," +
				$"\"medications\":\"{_medications}\"," +
				$"\"physicalEvaluation\":\"{_physicalEvaluation}\"}}," +
				$"\"movements\":{strMovements}}}}}";
		}

		public override string ToString()
		{
			CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

			string strMovements = "[";

			foreach (Movement movement in _movements)
			{
				strMovements += $"{movement},";
			}
			strMovements = $"{strMovements.TrimEnd(',')}]";

			return $"{{\"session\":{{\"id\":\"{_id}\"," +
				$"\"title\":\"{_title}\"," +
				$"\"description\":\"{_description}\"," +
				$"\"professionalId\":\"{_professionalId}\"," +
				$"\"patientSessionNumber\":{_patientSessionNumber}," +
				"\"app\":{" +
				$"\"code\":{_appCode}," +
				$"\"data\":\"{_appData}\"}}," +
				"\"patient\":{" +
				$"\"id\":\"{_patientId}\"," +
				$"\"age\":{_patientAge}," +
				$"\"height\":{_patientHeight}," +
				$"\"weight\":{_patientWeight}}}," +
				"\"medicalData\":{" +
				$"\"mainComplaint\":\"{_mainComplaint}\"," +
				$"\"historyOfCurrentDesease\":\"{_historyOfCurrentDesease}\"," +
				$"\"historyOfPastDesease\":\"{_historyOfPastDesease}\"," +
				$"\"diagnosis\":\"{_diagnosis}\"," +
				$"\"relatedDeseases\":\"{_relatedDeseases}\"," +
				$"\"medications\":\"{_medications}\"," +
				$"\"physicalEvaluation\":\"{_physicalEvaluation}\"}}," +
				$"\"articulationData\":{strMovements}}}}}";
		}
	}
}