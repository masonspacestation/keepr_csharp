# Keepr
Full Stack Application: Vue.js, C#, MySQL, API

#### Andrew Mason
#### Completed June 2024
#### Live Site: [Keepr](https://keepr_csharp.andrewmason.dev/)

***

Keepr is a social network that allows users to visually share and discover new interests by posting images or videos to their own, or others', collections (i.e., a collection of 'vaults,' usually with a common theme) and browsing what other users have kept.

### New Skills
Keepr was the most complex app I had created to date. I learned to make a plan and document my work as I go in order to make sure I kept things consistent and didn't miss any connections.

### Create Posts
![User flow creating a post](https://andrewmason.dev/keepr-img-1.png)
Add and sort images (keeps) to share with anyone visiting the Keepr site or your profile.


### Keepr Community
Browse others’ posts and add them to your vaults. Posts retain a count of how many times they’ve been viewed, and how many times they’ve been added to a vault.

### Requirements
- Users can Register, login and are automatically authenticated on refresh

- Visitors can see all keeps (login not required)

- All users have a public profile page

- Logged in users can:
  - Create and Delete Keeps
  - Create and Delete Vaults
  - Add Keeps to Vaults
  - Edit their account and public profile

- From the Vault Page, users can see and remove keeps from their vaults

- The user should be pushed back to the home page when they attempt to open a Vault Page for a vault they do not have access to

- Clicking on a keep card opens the keep in a modal
- Anytime a keep is kept in a vault the kept count is incremented
- Anytime a keeps details are viewed, the keep's view count is incremented

- Back end must pass all Postman tests for valid authorization, no authorization, and invalid authorization

- Ul adheres to Web UI, Accessibility, and Professional Standards
