import { Meta, StoryFn } from "@storybook/react";
import { TerminalSingle } from "features/common/terminal/TerminalSingle";

export default {
  title: "Features/Common/Terminal/TerminalSingle",
  component: TerminalSingle,
  args: {
    name: "Example",
    color: "grey",
    maxQuantity: 2,
    direction: "Bidirectional",
  },
} as Meta<typeof TerminalSingle>;

const Template: StoryFn<typeof TerminalSingle> = (args) => <TerminalSingle {...args} />;

export const Default = Template.bind({});
