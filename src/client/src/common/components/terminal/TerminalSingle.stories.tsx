import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalSingle } from "common/components/terminal/TerminalSingle";

export default {
  title: "Common/Terminal/TerminalSingle",
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
