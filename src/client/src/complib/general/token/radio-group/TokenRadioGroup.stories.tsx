import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TokenRadioGroup } from "complib/general/token/radio-group/TokenRadioGroup";
import { TokenRadioGroupItem } from "complib/general/token/radio-group/TokenRadioGroupItem";

export default {
  title: "General/TokenRadioGroup",
  component: TokenRadioGroup,
  subcomponents: { TokenRadioGroupItem },
} as ComponentMeta<typeof TokenRadioGroup>;

const Template: ComponentStory<typeof TokenRadioGroup> = (args) => <TokenRadioGroup {...args} />;

export const Default = Template.bind({});
Default.args = {
  onValueChange: (x) => alert(`[STORYBOOK] TokenRadioGroup.onValueChange: ${x}`),
  children: (
    <>
      <TokenRadioGroupItem value={"a"}>Choice A</TokenRadioGroupItem>
      <TokenRadioGroupItem value={"b"}>Choice B</TokenRadioGroupItem>
      <TokenRadioGroupItem value={"c"}>Choice C</TokenRadioGroupItem>
    </>
  ),
};

export const WithChecked = Template.bind({});
WithChecked.args = {
  children: (
    <>
      <TokenRadioGroupItem value={"a"}>Choice A</TokenRadioGroupItem>
      <TokenRadioGroupItem checked value={"b"}>
        Choice B
      </TokenRadioGroupItem>
      <TokenRadioGroupItem value={"c"}>Choice C</TokenRadioGroupItem>
    </>
  ),
};
