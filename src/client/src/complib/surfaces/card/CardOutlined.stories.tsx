import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Card } from "./Card";
import { Box, Flexbox } from "../../layouts";
import { Heading, Text } from "../../text";
import { Button } from "../../buttons";

export default {
  title: "Surfaces/Card/Outlined",
  component: Card,
  args: {
    variant: "outlined",
  },
} as ComponentMeta<typeof Card>;

const Template: ComponentStory<typeof Flexbox> = (args) => (
  <Box width={"250px"}>
    <Card {...args}>
      <Flexbox flexDirection={"column"} gap={"20px"}>
        <Heading variant={"title-large"}>A rather nice card</Heading>
        <Text>A card utilized both the card and layout components.</Text>
        <Button>Action</Button>
      </Flexbox>
    </Card>
  </Box>
);

export const Default = Template.bind({});

export const Interactive = Template.bind({});
Interactive.args = {
  interactive: true,
};

export const Squared = Template.bind({});
Squared.args = {
  square: true,
};

export const ElevatedLevel1 = Template.bind({});
ElevatedLevel1.args = {
  elevation: 1,
};

export const ElevatedLevel2 = Template.bind({});
ElevatedLevel2.args = {
  elevation: 2,
};

export const ElevatedLevel3 = Template.bind({});
ElevatedLevel3.args = {
  elevation: 3,
};

export const ElevatedLevel4 = Template.bind({});
ElevatedLevel4.args = {
  elevation: 4,
};

export const ElevatedLevel5 = Template.bind({});
ElevatedLevel5.args = {
  elevation: 5,
};
