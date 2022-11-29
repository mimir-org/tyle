import { ComponentMeta, ComponentStory } from "@storybook/react";
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
} as ComponentMeta<typeof TerminalSingle>;

const Template: ComponentStory<typeof TerminalSingle> = (args) => <TerminalSingle {...args} />;

export const Default = Template.bind({});
