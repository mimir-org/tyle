import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TransportPreview } from "common/components/transport/TransportPreview";

export default {
  title: "Common/Transport/TransportPreview",
  component: TransportPreview,
} as ComponentMeta<typeof TransportPreview>;

const Template: ComponentStory<typeof TransportPreview> = (args) => <TransportPreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Transport",
  aspectColor: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  transportColor: faker.color.rgb(),
};
