from django.db import models

class Restaurant(models.Model):
  name = models.CharField(max_length=255)
  lat = models.FloatField()
  lnt = models.FloatField()