import { ConditionalWrapper } from "./ConditionalWrapper";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Card } from "../../surfaces";
import { Button } from "../../buttons";

export default {
  title: "Utils/ConditionalWrapper",
  component: ConditionalWrapper,
  args: {
    wrapper: (c) => <Card>{c}</Card>,
    children: <Button>Wrapped?</Button>,
  },
} as ComponentMeta<typeof ConditionalWrapper>;

const Template: ComponentStory<typeof ConditionalWrapper> = (args) => <ConditionalWrapper {...args} />;

export const Example = Template.bind({});
