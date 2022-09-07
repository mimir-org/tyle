import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TransportPreview } from "./TransportPreview";

export default {
  title: "Content/Common/Transport/TransportPreview",
  component: TransportPreview,
} as ComponentMeta<typeof TransportPreview>;

const Template: ComponentStory<typeof TransportPreview> = (args) => <TransportPreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Transport",
  color: faker.color.rgb(),
};
