import { Meta, StoryFn } from "@storybook/react";
import { Button } from "complib/buttons";
import { Box, Flexbox } from "complib/layouts";
import { Card } from "complib/surfaces/card/Card";
import { Heading, Text } from "complib/text";

export default {
  title: "Surfaces/Card",
  component: Card,
} as Meta<typeof Card>;

const Template: StoryFn<typeof Flexbox> = (args) => (
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
