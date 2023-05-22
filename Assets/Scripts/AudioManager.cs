using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); //temporary gameObject vide
        tempGO.transform.position = pos; //les positions de ce GO se mettent � la position pos
        AudioSource audioSource = tempGO.AddComponent<AudioSource>(); //ajoute l'audio source � l'objet et on stocke les infos
        audioSource.clip = clip; //charger clip de l'audio source
        audioSource.outputAudioMixerGroup = soundEffectMixer; // acc�der � l'output et lui donner l'output de la piste audio
        audioSource.Play(); 
        Destroy(tempGO, clip.length); //supp objet une fois le son jou�
        return audioSource;
    }
}