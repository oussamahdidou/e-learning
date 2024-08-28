import { animate, animateChild, group, query, style, transition, trigger } from "@angular/animations";

export const slideInAnimation =
trigger('routeAnimations', [
  transition('register => student-register', slideTo('right')),
  transition('register => teacher-register', slideTo('right')),
  transition('student-register => register', slideTo('left')),
  transition('teacher-register => register', slideTo('left')),


]);
// trigger('routeAnimations', [
//     transition('register <=> student-register', [
//       style({ position: 'relative' }),
//       query(':enter, :leave', [
//         style({
//           position: 'absolute',
//           top: 0,
//           left: 0,
//           width: '100%'
//         })
//       ], { optional: true }),
//       query(':enter', [
//         style({ left: '-100%' })
//       ], { optional: true }),
//       query(':leave', animateChild(), { optional: true }),
//       group([
//         query(':leave', [
//           animate('200ms ease-out', style({ left: '100%', opacity: 0 }))
//         ], { optional: true }),
//         query(':enter', [
//           animate('300ms ease-out', style({ left: '0%' }))
//         ], { optional: true }),
//         query('@*', animateChild(), { optional: true })
//       ]),
//     ]),


// ]);
function slideTo(direction: string){
  const optional = {optional: true};
  return [
    query(':enter, :leave',[
      style({
        position:'absolute',
        top:0,
        [direction]:0,
        width:'100%'
      })
    ],optional),
    query(':enter',[
      style({
        [direction]:'-100%'
      })
    ]),
    group([
      query(':leave',[
        animate('600ms ease',style({[direction]:'100%'}))
      ],optional),
      query(':enter',[
        animate('600ms ease',style({[direction]:'0%'}))
      ]),
    ])

  ];
}