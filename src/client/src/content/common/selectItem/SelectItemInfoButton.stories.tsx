import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "../../../assets/icons/modules";
import { SelectItemInfoButton } from "./SelectItemInfoButton";

export default {
  title: "Content/Common/SelectItem/SelectItemInfoButton",
  component: SelectItemInfoButton,
} as ComponentMeta<typeof SelectItemInfoButton>;

const Template: ComponentStory<typeof SelectItemInfoButton> = (args) => <SelectItemInfoButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Pressure, absolute",
  color: "orange",
  traits: {
    condition: "Maximum",
    qualifier: "Operating",
    source: "Calculated",
  },
};

export const Actionable = Template.bind({});
Actionable.args = {
  ...Default.args,
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] AttributeDescription.Remove"),
};
