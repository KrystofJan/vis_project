using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ORM;
using Models;

namespace Generator
{
    public class StorageGenerate
    {
        public static List<String> Gods { get; } = new List<String>
        {
            "Zeus", "Hera", "Poseidon", "Demeter", "Hades", "Hestia", "Apollo", "Artemis", "Ares", "Athena",
            "Hermes", "Dionysus", "Aphrodite", "Hephaestus", "Persephone", "Eros", "Nyx", "Erebus", "Thanatos",
            "Hypnos", "Morpheus", "Charon", "Helios", "Selene", "Pan", "Hecate", "Eos", "Nike", "Iris", "Nemesis",
            "Eris", "Tyche", "Hebe", "Phobos", "Deimos", "Calliope", "Clio", "Erato", "Euterpe", "Melpomene",
            "Polyhymnia", "Terpsichore", "Thalia", "Urania", "Aglaea", "Euphrosyne", "Thalia", "Clotho", "Lachesis",
            "Atropos", "Alecto", "Megaera", "Tisiphone", "Panacea", "Asclepius", "Epione", "Ganymede", "Harmonia",
            "Heracles", "Pandora", "Prometheus", "Epimetheus", "Icarus", "Narcissus", "Echo", "Hyacinth", "Leander",
            "Orpheus", "Theseus", "Atalanta", "Bellerophon", "Castor", "Pollux", "Daedalus", "Jason", "Medea",
            "Atreus", "Agamemnon", "Menelaus", "Oedipus", "Clytemnestra", "Electra", "Iphigenia", "Odysseus",
            "Penelope", "Circe", "Calypso", "Nestor", "Achilles", "Hector", "Priam", "Paris", "Andromache", "Cassandra",
            "Patroclus", "Laocoon", "Sisyphus", "Tantalus", "Oedipus", "Phaedra", "Perseus", "Ariadne", "Chiron",
            "Galatea", "Daphne", "Boreas", "Zephyr", "Eurus", "Notus", "Selene", "Helios", "Leto", "Rhea", "Oceanus"
        };

        public static Storage Generate()
        {
            Storage storage = new Storage();
            storage.address = AddressGenerate.Generate();

            Random random = new Random();
            string randomGod1 = Gods[random.Next(Gods.Count)];
            string randomGod2 = Gods[random.Next(Gods.Count)];
            storage.storage_name = randomGod1 + " " + randomGod2;
            int number_of_stocks = Faker.RandomNumber.Next(99);

            storage.storage_id = StorageDAO.Insert(storage);
            Collection<Stock> stocks = new Collection<Stock>();
            for (int i = 0; i < number_of_stocks; i++)
            {
                Stock stock = StockGenerate.GenerateForStorage(storage);
                stocks.Add(stock);
            }

            storage.stocks = stocks;
            return storage;
        }
    }
}