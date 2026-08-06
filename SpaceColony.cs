//Why are you lookin' at my code? Its totally legit. It won't give you a virus. Maybe. 
//Don't you trust me? I'm so very offended by you and your accusations.
//Y'know what? Fine. Now it will give you a virus. Go on. Run it. I dare you.


int food = 100, water = 100, oxygen = 100, energy = 100, colonists = 5, day = 1, greenhouses = 0, waterExtractors = 0, habitats = 0, defenseTowers = 0; 
int nukeLaunched = 0;

const int GREENHOUSE_ENERGY_COST = 20, WATER_EXTRACTORS_COST = 15, HABITAT_ENERGY_COST = 30, DEFENSE_TOWER_ENERGY_COST = 25, HABITAT_COLONISTS_INCREASE = 2;

const int GREENHOUSE_PRODUCTION_MULTIPLIER = 5, WATER_EXTRACTOR_PRODUCTION_MULTIPLIER = 5;
const int COLONIST_FOOD_COST = 2, COLONIST_ENERGY_COST = 1, COLONIST_WATER_COST = 2, COLONIST_OXYGEN_COST = 3;

const int GATHER_OXYGEN_MIN = 5, GATHER_OXYGEN_MAX = 15, GATHER_ENERGY_MIN = 5, GATHER_ENERGY_MAX = 15;

const int ALIEN_START_DAY = 3, ALIEN_MAX_DAMAGE = 4, DEFENSE_TOWER_MULTIPLIER = 2, ENERGY_LOSS_MULTIPLIER = 5;
const int FRIENDLY_ALIEN_FOOD_AMOUNT = 20, FRIENDLY_ALIEN_WATER_AMOUNT = 20;

Random rand = new Random();

Console.WriteLine("Welcome to Space Colony Manager Simulator: Version Ultra Delux 9000");
Console.WriteLine("©Copyright 1865. All rights reserved. Not really. maybe...");








void HandleBuild()
{
    Console.WriteLine(" ");
    Console.WriteLine("Available buildings:");
    Console.WriteLine(" 1. Greenhouse (Cost: " + GREENHOUSE_ENERGY_COST + " energy).");
    Console.WriteLine(" 2. Water Extractor (Cost: " + WATER_EXTRACTORS_COST + " energy).");
    Console.WriteLine(" 3. Habitat (Cost: " + HABITAT_ENERGY_COST + " energy).");
    Console.WriteLine(" 4. Defense Tower (Cost: " + DEFENSE_TOWER_ENERGY_COST + " energy).");
    Console.WriteLine(" 5. Nuclear Bomb (Cost: " + DEFENSE_TOWER_ENERGY_COST + " energy).");

    string buildChoice = Console.ReadLine();

    if (buildChoice == "1" && energy >= GREENHOUSE_ENERGY_COST)
    {
        greenhouses++;
        energy -= GREENHOUSE_ENERGY_COST;
        Console.WriteLine("Greenhouse built.");
    }

    else if (buildChoice == "2" && energy >= WATER_EXTRACTORS_COST)
    {
        waterExtractors++;
        energy -= WATER_EXTRACTORS_COST;
        Console.WriteLine("Water Extractor built.");
    }
    else if (buildChoice == "3" && energy >= HABITAT_ENERGY_COST)
    {
        habitats++;
        energy -= HABITAT_ENERGY_COST;
        colonists += HABITAT_COLONISTS_INCREASE;
        Console.WriteLine("Habitat built. " + HABITAT_COLONISTS_INCREASE + " colonists joined.");
    }

    else if (buildChoice == "4" && energy >= DEFENSE_TOWER_ENERGY_COST)
    {
        defenseTowers++;
        energy -= DEFENSE_TOWER_ENERGY_COST;
        Console.WriteLine("Defense Tower built.");
    }

    else if (buildChoice == "5" && energy >= DEFENSE_TOWER_ENERGY_COST)
    {
        Console.WriteLine("You construct a nuclear weapons. The nuclear bomb tests have scared the aliens away for 2 days. ");
        Console.WriteLine("-30 energy. 1 colonist(s) have died during testing.");
        colonists--;
        nukeLaunched = 2;
        energy -= DEFENSE_TOWER_ENERGY_COST;
    }


    else if (buildChoice == "6")
    {
        Console.WriteLine("5 isn't an option, stupid.");
    }

    else
    {
        Console.WriteLine("Not enough energy or invalid choice.");
    }
}

void HandleGather()
{
    int gatheredOxygen = rand.Next(GATHER_OXYGEN_MIN, GATHER_OXYGEN_MAX);
    int gatheredEnergy = rand.Next(GATHER_ENERGY_MIN, GATHER_ENERGY_MAX);

    oxygen += gatheredOxygen;
    energy += gatheredEnergy;

    Console.WriteLine("Resources gathered: " + gatheredOxygen + " oxygen, " + gatheredEnergy + " energy.");
}



void HandleSkip()
{
    Console.WriteLine("Skipping to next day...");
}




while (colonists > 0)
{
    Console.WriteLine("");
    Console.WriteLine("Day: " + day);
    Console.WriteLine("Colonists: " + colonists);

    int avaliableColonists = colonists;

    int avaliableGreenhouses = greenhouses;
    if (avaliableColonists < avaliableGreenhouses)
    {
        avaliableGreenhouses = avaliableColonists;
    }
    avaliableColonists -= avaliableGreenhouses;

    int avaliableWaterExtractors = waterExtractors;
    if (avaliableColonists < avaliableWaterExtractors)
    {
        avaliableWaterExtractors = avaliableColonists;
    }
    avaliableColonists -= avaliableWaterExtractors;

    int avaliableDefenseTowers = defenseTowers;
    if (avaliableColonists < avaliableDefenseTowers)
    {
        avaliableDefenseTowers = avaliableColonists;
    }
    avaliableColonists -= avaliableDefenseTowers;

    food += avaliableGreenhouses * GREENHOUSE_PRODUCTION_MULTIPLIER;
    water += avaliableWaterExtractors * WATER_EXTRACTOR_PRODUCTION_MULTIPLIER;
    oxygen += avaliableGreenhouses * GREENHOUSE_PRODUCTION_MULTIPLIER;

    food -= colonists * COLONIST_FOOD_COST;
    water -= colonists * COLONIST_WATER_COST;
    oxygen -= colonists * COLONIST_OXYGEN_COST;
    energy -= colonists * COLONIST_ENERGY_COST;

    if (food < 0 || water < 0 || oxygen < 0 || energy < 0)
    {
        colonists--;

        if (food < 0)
        {
            food = 0;
        }

        if (water < 0)
        {
            water = 0;
        }

        if (oxygen < 0)
        {
            oxygen = 0;
        }

        if (energy < 0)
        {
            energy = 0;
        }


        Console.WriteLine("A colonist has died due to shortages of supplies.");
    }

    if (day >= ALIEN_START_DAY || nukeLaunched <= 0)
    {
        int chance = rand.Next(1, 5);
        if (chance == 1)
        {
            string species = "";
            int type = rand.Next(0, 2);

            if (type == 1)
            {
                species = "Groz";
            }

            else
            {
                species = "Itnev";
            }

            Console.WriteLine("Alien encounter. Species: " + species + " - ");

            if (type == 0)
            {
                int ATTACK_STRENGTH = day / ALIEN_START_DAY + rand.Next(1, ALIEN_MAX_DAMAGE);
                int defensePower = avaliableDefenseTowers * DEFENSE_TOWER_MULTIPLIER;

                Console.WriteLine("Hostile");
                Console.WriteLine("Attack strength: " + ATTACK_STRENGTH + " | Your defense strength: " + defensePower);
                int loss = rand.Next(-defensePower, ATTACK_STRENGTH);

                if (loss <= 0)
                {
                    Console.WriteLine("Defense Tower(s) repelled the alien attack.");
                }
                else
                {
                    int colonistLoss = 1;
                    if (loss > 2)
                    {
                        colonistLoss = 2;
                    }

                    colonists -= colonistLoss;
                    energy -= loss * ENERGY_LOSS_MULTIPLIER;

                    if (colonists < 0)
                    {
                        colonists = 0;
                    }

                    if (energy < 0)
                    {
                        energy = 0;
                    }

                    Console.WriteLine("Alien attack has breached defenses. " + loss + " damage. Lost: " + colonistLoss + " colonist(s), -" + (loss * 5) + " energy");
                }
            }
            else
            {
                Console.WriteLine("Friendly.");
                food += FRIENDLY_ALIEN_FOOD_AMOUNT;
                water += FRIENDLY_ALIEN_WATER_AMOUNT;
                Console.WriteLine("Aliens brought supplies: " + FRIENDLY_ALIEN_FOOD_AMOUNT + " food, " + FRIENDLY_ALIEN_WATER_AMOUNT + " water.");
            }
        }
    }



    Console.WriteLine("Food: " + food + " | Water: " + water + " | Oxygen: " + oxygen + " | Energy: " + energy);
    Console.WriteLine("Buildings: ");
    Console.WriteLine(" Greenhouses: " + greenhouses + " | Active: " + avaliableGreenhouses);
    Console.WriteLine(" Water Extractors: " + waterExtractors + " | Active: " + avaliableWaterExtractors);
    Console.WriteLine(" Habitats: " + habitats);
    Console.WriteLine(" Defense Towers: " + defenseTowers + " | Active: " + avaliableDefenseTowers);
    Console.WriteLine(" ");
    Console.WriteLine("Choose action: ( build / gather / skip / murder / mcdonalds )");

    string action = Console.ReadLine();

    if (action == "build")
    {
        HandleBuild();
    }

    if (action == "gather")
    {
        HandleGather();
    }

    if (action == "supersecretpassword1234*")
    {
        Console.WriteLine("Secret Code unlocked. Congratulations. You get +30 oxygen!");
        oxygen += HABITAT_ENERGY_COST;
    }

    if (action == "murder")
    {
        Console.WriteLine("You murdered all the colonists. Now you have no one to sustain your habitats, idiot!");
        colonists = 0;
    }

    if (action == "mcdonalds")
    {
        Console.WriteLine("You get some McDonalds. +30 (McDonalds-fueled energy). 1 colonist(s) have died from heart disease/high colesterol.");
        colonists--;
        energy += HABITAT_ENERGY_COST;
    }

    if (action == "skip")
    {
        HandleSkip();
    }

    else
    {
        Console.WriteLine("Command not found. Please use the listed commands ( build / gather / skip / murder / mcdonalds )");
    }

    day++;
    nukeLaunched--;
}

Console.WriteLine("All colonists have perished. Game over.");
