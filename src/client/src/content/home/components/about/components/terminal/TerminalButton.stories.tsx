import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalButton } from "./TerminalButton";

export default {
  title: "Content/Home/About/Terminal/TerminalButton",
  component: TerminalButton,
  args: {
    color: "grey",
  },
} as ComponentMeta<typeof TerminalButton>;

const Template: ComponentStory<typeof TerminalButton> = (args) => <TerminalButton {...args} />;

export const Default = Template.bind({});

export const VariantInput = Template.bind({});
VariantInput.args = {
  variant: "Input",
};

export const VariantOutput = Template.bind({});
VariantOutput.args = {
  variant: "Output",
};

export const VariantBidirectional = Template.bind({});
VariantBidirectional.args = {
  variant: "Bidirectional",
};

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};
