using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxEnergy = 100;
    public int currentEnergy;
    public EnergyBar energyBar;

    // oyun baþladýðýnda saðlýk maximum
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

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
        currentEnergy -= damage;

        energyBar.SetEnergy(currentEnergy);
    }
}
