import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { InfoItemButton } from "./InfoItemButton";

export default {
  title: "Common/InfoItem/InfoItemButton",
  component: InfoItemButton,
} as ComponentMeta<typeof InfoItemButton>;

const Template: ComponentStory<typeof InfoItemButton> = (args) => <InfoItemButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Pressure, absolute",
  color: "orange",
  descriptors: {
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
  onAction: () => alert("[STORYBOOK] ItemInfoButton.onAction"),
};
