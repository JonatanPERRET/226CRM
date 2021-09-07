using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CrmBusiness
{
    public class TestContactListManager
    {
        #region private attributes
        private ContactListManager _contactListManager = null;
        private List<Contact> _listOfContactsInitial = null;
        private List<Contact> _listOfContactsToAdd = null;
        #endregion private attributes

        [SetUp]
        public void Setup()
        {
            //create lists of contacts for testing purpose
            _listOfContactsInitial = CreateListOfContacts(10, 1);
            _listOfContactsToAdd = CreateListOfContacts(5, 10);
        }

        private List<Contact> CreateListOfContacts(int amountOfContacts, int startIndex)
        {
            List<Contact> listOfContacts = new List<Contact>();

            for(int i = startIndex; i < amountOfContacts + startIndex; i++)
            {
                Contact contact = new Contact("Name" + i, "Firstname" + i, new DateTime(1900, 01, i), "Nationality" + i, "email" + i + "@mail.org");
                listOfContacts.Add(contact);
            }
            return listOfContacts;
        }

        [Test]
        public void Contacts_GetValue_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = _listOfContactsInitial;

            _contactListManager = new ContactListManager(_listOfContactsInitial);

            //when
            actualContacts = _contactListManager.Contacts;

            //then
            for (int i = 0; i < expectedContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);           
            }
        }

        [Test]
        public void Contacts_EmptyListOfContacts_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;

            _contactListManager = new ContactListManager();

            //when
            actualContacts = _contactListManager.Contacts;
            
            //then
            Assert.IsNull(actualContacts);
        }

        [Test]
        public void Add_EmptyContactManager_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = _listOfContactsInitial;

            _contactListManager = new ContactListManager();

            //when
            _contactListManager.Add(_listOfContactsInitial);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < actualContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Add_NotEmptyContactManagerNoDuplicate_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = new List<Contact>(_listOfContactsInitial);

            _contactListManager = new ContactListManager(_listOfContactsInitial);
            expectedContacts.AddRange(_listOfContactsInitial);

            //when
            _contactListManager.Add(_listOfContactsInitial);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < actualContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Add_AddDuplicateNotAllowed_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = new List<Contact>(_listOfContactsInitial);

            _contactListManager = new ContactListManager(_listOfContactsInitial);

            //when
            _contactListManager.Add(_listOfContactsInitial);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < actualContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Add_AddDuplicateAllowed_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = new List<Contact>(_listOfContactsInitial);

            _contactListManager = new ContactListManager(_listOfContactsInitial);
            expectedContacts.AddRange(_listOfContactsInitial);

            //when
            _contactListManager.Add(_listOfContactsInitial, true);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < expectedContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Remove_NominalCase_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = new List<Contact>(_listOfContactsInitial);
            List<Contact> contactToRemove = new List<Contact> { _listOfContactsInitial[0] };

            _contactListManager = new ContactListManager(_listOfContactsInitial);
            expectedContacts.Remove(contactToRemove[0]);
            
            //when
            _contactListManager.Remove(contactToRemove);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < expectedContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Remove_RemoveContactsContainDuplicate_Success()
        {
            //given
            //refere to Setup method
            List<Contact> actualContacts = null;
            List<Contact> expectedContacts = new List<Contact>(_listOfContactsInitial);
            List<Contact> contactDuplicateToRemove = new List<Contact> { _listOfContactsInitial[0] };
            
            expectedContacts.Remove(contactDuplicateToRemove[0]);
            _contactListManager = new ContactListManager(_listOfContactsInitial);
            _contactListManager.Add(contactDuplicateToRemove, true);

            //when
            _contactListManager.Remove(contactDuplicateToRemove);

            //then
            actualContacts = _contactListManager.Contacts;

            for (int i = 0; i < expectedContacts.Count; i++)
            {
                Assert.AreEqual(expectedContacts[i].Email, actualContacts[i].Email);
            }
        }

        [Test]
        public void Remove_EmptyContactsList_ThrowRemoveContactException()
        {
            //given
            //refere to Setup method
            _contactListManager = new ContactListManager();

            //when + then
            Assert.Throws<RemoveContactException>(delegate 
            { 
                _contactListManager.Remove(_listOfContactsInitial); 
            });
        }
    }
}