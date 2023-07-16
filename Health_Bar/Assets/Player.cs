using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;

	// oyun baþladýðýnda saðlýk maximum
	void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

	// boþluk uþuyla hasarverir
	// boþluk tuþuyla güncelleme birkere çaðrýlýr
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
			TakeDamage(20);
        }
    }

	// hasarý mevcut saðlýktan çýkarýyor
	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}
