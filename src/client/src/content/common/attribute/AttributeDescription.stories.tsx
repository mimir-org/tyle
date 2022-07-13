import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "../../../assets/icons/modules";
import { AttributeDescription } from "./AttributeDescription";

export default {
  title: "Content/Common/Attribute/AttributeDescription",
  component: AttributeDescription,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as ComponentMeta<typeof AttributeDescription>;

const Template: ComponentStory<typeof AttributeDescription> = (args) => <AttributeDescription {...args} />;

export const Default = Template.bind({});

export const Actionable = Template.bind({});
Actionable.args = {
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] AttributeDescription.Remove"),
};
