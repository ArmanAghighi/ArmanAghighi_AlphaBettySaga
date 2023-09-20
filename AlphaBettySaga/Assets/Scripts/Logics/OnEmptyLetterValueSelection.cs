using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OnEmptyLetterValueSelection : MonoBehaviour
{
    private TextMeshPro _letterMesh;
    private TextMeshPro _valueMesh;
    public int _valueIndex = 0;

    Dictionary<char, int> _letterValues = new Dictionary<char, int>()
{
    { 'A', 1 },
    { 'B', 3 },
    { 'C', 2 },
    { 'D', 2 },
    { 'E', 1 },
    { 'F', 3 },
    { 'G', 3 },
    { 'H', 2 },
    { 'I', 1 },
    { 'J', 4 },
    { 'K', 4 },
    { 'L', 2 },
    { 'M', 3 },
    { 'N', 3 },
    { 'O', 1 },
    { 'P', 3 },
    { 'Q', 4 },
    { 'R', 3 },
    { 'S', 3 },
    { 'T', 1 },
    { 'U', 2 },
    { 'V', 3 },
    { 'W', 3 },
    { 'X', 4 },
    { 'Y', 3 },
    { 'Z', 4 }
};
    Dictionary<char, int> _vowsLetterValues = new Dictionary<char, int>()
{
    { 'A', 1 },
    { 'E', 1 },
    { 'I', 1 },
    { 'O', 1 },
    { 'U', 2 },
};
    private void Awake()
    {
        _valueIndex = Random.Range(1, 101);
        _letterMesh = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        _valueMesh = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        if (int.Parse(gameObject.transform.parent.name) % 3 != 0)
        {
            char randomLetter = GetRandomNormalLetter();
            int randomValue = _letterValues[randomLetter];
            _letterMesh.text = randomLetter.ToString();
            _valueMesh.text = randomValue.ToString();
        }
        else
        {
            char randomLetter = GetRandomVowsLetter();
            int randomValue = _vowsLetterValues[randomLetter];
            _letterMesh.text = randomLetter.ToString();
            _valueMesh.text = randomValue.ToString();
        }
        if (_valueIndex % 5 == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            _valueIndex = 2;
        }
        else if (_valueIndex % 30 == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            _valueIndex = 1;
        }
        else
        {
            _valueIndex = 0;
        }
    }

    private char GetRandomNormalLetter()
    {
        System.Random random = new System.Random();
        List<char> keys = new List<char>(_letterValues.Keys);
        int randomIndex = random.Next(keys.Count);
        char randomLetter = keys[randomIndex];
        return randomLetter;
    }
    private char GetRandomVowsLetter()
    {
        System.Random random = new System.Random();
        List<char> keys = new List<char>(_vowsLetterValues.Keys);
        int randomIndex = random.Next(keys.Count);
        char randomLetter = keys[randomIndex];
        return randomLetter;
    }
}
