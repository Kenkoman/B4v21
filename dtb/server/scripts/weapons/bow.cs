function arrowProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal) {
  //echo("hooray");
  tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}

bow.threatlevel = "Normal";
addWeapon(bow);

bowImage.deathAnimationClass = "projectile";
bowImage.deathAnimation = "death5";
bowImage.deathAnimationPercent = 0.4;