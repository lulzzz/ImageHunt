var builder = require('botbuilder');

module.exports = [
  function(session) {
    session.send('Bonjour ' + session + ', je suis le bot qui va vous aider � jouer � la chasse au tr�sor.');
  },
function(session) {
  session.send('vous faites partie de l\'�quipe ');
  builder.Prompts.choice(session, 'Vous confirmez ?', ['Oui', 'Non']);
}
  ]