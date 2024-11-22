'use client';

import { useAppSelector } from '@hooks/reduxHooks';
import { selectSportClasses } from '@slices/sportClass/sportClassSlice';
import { ExtendedSportClass } from '@/common/types/sportClassModels';
import { Accordion, AccordionItem } from '@nextui-org/accordion';

interface AdminDashboardProps {
  sportClasses: ExtendedSportClass[]; // Array of sport class objects
}
export default function AdminDashboard({ sportClasses }: AdminDashboardProps) {
  if (!sportClasses || sportClasses.length === 0) {
    return <p>No sport classes available.</p>;
  }

  return (
    <div className="flex justify-center items-start h-screen pt-20">
      <div className="w-full max-w-2xl px-4">
        <Accordion className="space-y-4">
          {sportClasses.map((sportClass) => (
            <AccordionItem
              key={sportClass.id}
              title={`${sportClass.name} - ${sportClass.date}`}
              className="w-full text-sm text-left"
            >
              <div className="text-sm">
                <p>
                  <strong>Instructor:</strong>{' '}
                  {sportClass.instructorName || 'N/A'}
                </p>
                <p>
                  <strong>Price:</strong> $
                  {sportClass.price ? sportClass.price.toFixed(2) : 'N/A'}
                </p>
                <p>
                  <strong>Paid Enrollments:</strong>{' '}
                  {sportClass.paidEnrollments ?? '0'}
                </p>
              </div>
            </AccordionItem>
          ))}
        </Accordion>
      </div>
    </div>
  );
}
