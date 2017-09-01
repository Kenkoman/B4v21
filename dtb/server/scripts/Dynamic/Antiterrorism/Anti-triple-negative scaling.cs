package Wiggy_Anti_triple_negative_scaling {
function Player::setScale(%this, %scale) {
if(getWord(%scale, 0) < 0 && getWord(%scale, 1) < 0 && getWord(%scale, 2) < 0)
  %scale = "0 0 0";
for(%i = 0; %i <= 3; %i++) {
  if(getWord(%scale, %i) > 100)
    %scale = setWord(%scale, %i, 100);
}
Parent::setScale(%this, %scale);
}
function AIPlayer::setScale(%this, %scale) {
if(getWord(%scale, 0) < 0 && getWord(%scale, 1) < 0 && getWord(%scale, 2) < 0)
  %scale = "0 0 0";
for(%i = 0; %i <= 3; %i++) {
  if(getWord(%scale, %i) > 100)
    %scale = setWord(%scale, %i, 100);
}
Parent::setScale(%this, %scale);
}
function Vehicle::setScale(%this, %scale) {
if(getWord(%scale, 0) < 0 && getWord(%scale, 1) < 0 && getWord(%scale, 2) < 0)
  %scale = "0 0 0";
for(%i = 0; %i <= 3; %i++) {
  if(getWord(%scale, %i) > 100)
    %scale = setWord(%scale, %i, 100);
}
Parent::setScale(%this, %scale);
}
};
activatepackage(Wiggy_Anti_triple_negative_scaling);