import { Meta, StoryFn } from "@storybook/react";
import { TerminalButton } from "features/common/terminal/TerminalButton";

export default {
  title: "Features/Common/Terminal/TerminalButton",
  component: TerminalButton,
  args: {
    color: "grey",
  },
} as Meta<typeof TerminalButton>;

const Template: StoryFn<typeof TerminalButton> = (args) => <TerminalButton {...args} />;

export const Default = Template.bind({});

export const VariantInput = Template.bind({});
VariantInput.args = {
  direction: "Input",
};

export const VariantOutput = Template.bind({});
VariantOutput.args = {
  direction: "Output",
};

export const VariantBidirectional = Template.bind({});
VariantBidirectional.args = {
  direction: "Bidirectional",
};

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};
