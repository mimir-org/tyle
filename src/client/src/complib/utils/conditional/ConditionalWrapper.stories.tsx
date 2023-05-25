import { Meta, StoryFn } from "@storybook/react";
import { Button } from "complib/buttons";
import { Card } from "complib/surfaces";
import { ConditionalWrapper } from "./ConditionalWrapper";

export default {
  title: "Utils/ConditionalWrapper",
  component: ConditionalWrapper,
  args: {
    wrapper: (c) => <Card>{c}</Card>,
    children: <Button>Wrapped?</Button>,
  },
} as Meta<typeof ConditionalWrapper>;

const Template: StoryFn<typeof ConditionalWrapper> = (args) => <ConditionalWrapper {...args} />;

export const Example = Template.bind({});
