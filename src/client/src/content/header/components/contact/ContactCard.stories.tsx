import { ComponentStory } from "@storybook/react";
import { ContactCard } from "./ContactCard";

export default {
  title: "Content/Header/Contact/ContactCard",
  component: ContactCard,
};

const Template: ComponentStory<typeof ContactCard> = (args) => <ContactCard {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Threepwood",
  email: "guybrush.threepwood@island.com",
};
