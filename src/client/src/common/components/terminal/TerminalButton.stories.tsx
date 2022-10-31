import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalButton } from "common/components/terminal/TerminalButton";

export default {
  title: "Common/Terminal/TerminalButton",
  component: TerminalButton,
  args: {
    color: "grey",
  },
} as ComponentMeta<typeof TerminalButton>;

const Template: ComponentStory<typeof TerminalButton> = (args) => <TerminalButton {...args} />;

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
