from django.db import models

class User(models.Model):
  id = models.IntegerField(primary_key=True)
  name = models.CharField(max_length=255)


class UserCredential(models.Model):
  id = models.IntegerField(primary_key=True)
  userId = models.ForeignKey("User", on_delete=models.RESTRICT)
  account = models.CharField(max_length=12)
  password = models.CharField(max_length=12)
  salt = models.CharField(max_length=255)
