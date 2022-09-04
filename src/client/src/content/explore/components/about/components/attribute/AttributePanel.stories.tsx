import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockAttributeItem } from "../../../../../../utils/mocks";
import { AttributePanel } from "./AttributePanel";

export default {
  title: "Content/Explore/About/AttributePanel",
  component: AttributePanel,
  args: {
    ...mockAttributeItem(),
  },
} as ComponentMeta<typeof AttributePanel>;

const Template: ComponentStory<typeof AttributePanel> = (args) => <AttributePanel {...args} />;

export const Default = Template.bind({});
