- Instale o OpenLDAP
- Crie um container docker com o seguinte comando
sudo docker run -p 389:389 -p 636:636 --name openldap --env LDAP_ORGANISATION="Open Consult" --env LDAP_DOMAIN="openconsult.com" --env LDAP_ADMIN_PASSWORD="openconsult" --detach osixia/openldap:1.3.0
