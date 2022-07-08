import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "../../buttons";
import { Box, Flexbox } from "../../layouts";
import { Heading, Text } from "../../text";
import { Card } from "./Card";

export default {
  title: "Surfaces/Card",
  component: Card,
} as ComponentMeta<typeof Card>;

const Template: ComponentStory<typeof Flexbox> = (args) => (
  <Box width={"250px"}>
    <Card {...args}>
      <Flexbox flexDirection={"column"} gap={"20px"}>
        <Heading variant={"title-large"}>A rather nice card</Heading>
        <Text>
          {
            "This card's dimensions are constrained by its parent, use common layout components to populate it's surface"
          }
        </Text>
        <Button>Action</Button>
      </Flexbox>
    </Card>
  </Box>
);

export const Filled = Template.bind({});

export const Selected = Template.bind({});
Selected.args = {
  variant: "selected",
};
