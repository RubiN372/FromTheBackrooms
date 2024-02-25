using UnityEngine;

public class testscare : MonoBehaviour
{
    public AudioSource audios;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audios.volume = 1f;
            Vector3 newscale = new Vector3(.7f, .7f, .1f);
            gameObject.transform.localScale = newscale;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audios.volume = .8f;
            Vector3 newscale = new Vector3(.1f, .1f, .1f);
            gameObject.transform.localScale = newscale;
        }
    }
}
