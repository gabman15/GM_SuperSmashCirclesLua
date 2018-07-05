-- Circle class for each player

Circle = Class{}

function Circle:init(x, y, width, height)
    self.x = x
    self.y = y
    self.width = width
    self.height = height
    --[[ Which type of character the circle is
        0 - Red
        1 - Pink

        BETA
        probably different names for characters and there will be more characters
    ]]
    -- Default type is 0 (Red)
    self.type = 0

    -- Lives of the character (default starts at 5)
    self.lives = 5

    -- Speed of Circle
    self.dy = 0
    self.dx = 0
end

function Circle:setType(type)
    self.type = type
end