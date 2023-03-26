from django.db import models

class Restaurant(models.Model):
  id = models.IntegerField(primary_key=True)
  name = models.CharField(max_length=255)
  category = models.CharField(max_length=255)
  lnt = models.FloatField()
  lat = models.FloatField() 
  address = models.CharField(max_length=255)
  placeId = models.CharField(max_length=255)
  peiceLevel = models.FloatField()

  class Meta:
    db_table = 'Restaurant'