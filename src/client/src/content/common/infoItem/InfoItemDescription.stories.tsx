import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "../../../assets/icons/modules";
import { InfoItemDescription } from "./InfoItemDescription";

export default {
  title: "Content/Common/InfoItem/InfoItemDescription",
  component: InfoItemDescription,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    descriptors: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as ComponentMeta<typeof InfoItemDescription>;

const Template: ComponentStory<typeof InfoItemDescription> = (args) => <InfoItemDescription {...args} />;

export const Default = Template.bind({});

export const Actionable = Template.bind({});
Actionable.args = {
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] ItemDescription.onAction"),
};
