import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalSingle } from "./TerminalSingle";

export default {
  title: "Content/Common/Terminal/TerminalSingle",
  component: TerminalSingle,
  args: {
    name: "Example",
    color: "grey",
    amount: 2,
    direction: "Bidirectional",
  },
} as ComponentMeta<typeof TerminalSingle>;

const Template: ComponentStory<typeof TerminalSingle> = (args) => <TerminalSingle {...args} />;

export const Default = Template.bind({});
