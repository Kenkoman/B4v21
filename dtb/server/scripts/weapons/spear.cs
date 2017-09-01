function spearProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal) {
  tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}

spear.threatlevel = "Normal";
addWeapon(spear);

spearImage.deathAnimationClass = "projectile";
spearImage.deathAnimation = "death5";
spearImage.deathAnimationPercent = 0.4;