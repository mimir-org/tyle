import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { InterfacePreview } from "common/components/interface/InterfacePreview";

export default {
  title: "Common/Interface/InterfacePreview",
  component: InterfacePreview,
} as ComponentMeta<typeof InterfacePreview>;

const Template: ComponentStory<typeof InterfacePreview> = (args) => <InterfacePreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Interface",
  aspectColor: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  interfaceColor: faker.color.rgb(),
};
